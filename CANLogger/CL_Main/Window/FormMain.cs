﻿using CL_Framework;
using log4net;
using Sunisoft.IrisSkin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace CL_Main
{
    public partial class FormMain : Form
    {
        private static readonly ILog Logger = log4net.LogManager.GetLogger("info");

        private SkinEngine skinEngine;
        private DeviceGroup pDeviceGroup = DeviceGroup.CreateInstance();

        private List<FormData> pFormDatas = new List<FormData>();
        private FormDevice pFormDevice = new FormDevice();
        private FormStatus pFormStatus = new FormStatus();

        private Hashtable pNameDevicePairs = new Hashtable();
        //private Device selectedDevice = null;

        #region public apis

        public FormMain()
        {
            InitializeComponent();
        }

        public void AddDevice(Device device, object paras)
        {
            string deviceName = device.GetDeviceName();
            pNameDevicePairs.Add(deviceName, device);

            this.cbxSelectDevice.Items.Add(deviceName);
            for (uint channelIndex = 0; channelIndex < device.CANNum; channelIndex++)
            {
                Channel channel = device.GetChannel(channelIndex);
                FormData pFormData = new FormData(channel);
                pFormData.Show(this.dockPanel, DockState.Document);
                pFormDatas.Add(pFormData);
            }
            this.cbxSelectDevice.SelectedIndex = this.cbxSelectDevice.FindString(deviceName);
        }

        public void RemoveDevice(Device device, object paras)
        {
            string deviceName = device.GetDeviceName();
            pNameDevicePairs.Remove(deviceName);
            
            List<FormData> pFormDatas = FindMappingFormDatas(device);
            foreach (FormData pFormData in pFormDatas)
            {
                this.dockPanel.Controls.Remove(pFormData);
                this.pFormDatas.Remove(pFormData);
                pFormData.Close();
            }

            this.cbxSelectDevice.Items.Remove(deviceName);
            this.cbxSelectDevice.SelectedIndex = this.cbxSelectDevice.Items.Count > 0 ? 0 : -1;
        }

        public void UpdateDevice(Device device, object paras)
        {

        }

        #endregion

        #region private functions

        private void Init()
        {
            pDeviceGroup.DeviceAdded += new DeviceEventHandler(AddDevice);
            pDeviceGroup.DeviceRemoved += new DeviceEventHandler(RemoveDevice);
            pDeviceGroup.DeviceUpdated += new DeviceEventHandler(UpdateDevice);
        }

        private void Loading()
        {
            FormLoading formLoading = new FormLoading();
            formLoading.ShowDialog();
        }

        private void InitLoadSkinMenuItems()
        {
            skinEngine = new SkinEngine();
            List<string> skins = Directory.GetFiles(Application.StartupPath + @"\skins\", "*.ssk").ToList();

            int index = 0;
            skins.ForEach(skinFile =>
            {
                Logger.Info(string.Format("get skin file: [{0}]", skinFile));
                string itemText = Path.GetFileNameWithoutExtension(skinFile);
                ToolStripMenuItem skinItem = new ToolStripMenuItem(itemText);
                skinItem.Tag = skinFile;
                skinItem.Name = String.Concat("menuItemSkin", itemText.Replace(" ", ""));
                this.menuItemSkin.DropDownItems.Add(skinItem);
                index++;
            });
        }

        private void InitLoadControls()
        {
            InitLoadSkinMenuItems();

            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.Text += version;
            Logger.Info(string.Format("get CAN Logger version: [{0}]", version));

            menuItemLanguage.Visible = false;

            // init & load FormDevice
            pFormDevice.Show(this.dockPanel, DockState.DockBottom);
            //init & load FormStatus
            pFormStatus.Show(pFormDevice.Pane, DockAlignment.Right, 0.5);
        }

        private void SetLanguage(string language)
        {
            Logger.Info(string.Format("SetLanguage To {0}...", language));
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(language);
            System.ComponentModel.ComponentResourceManager resources = new ComponentResourceManager(this.GetType());

            resources.ApplyResources(this, "$this");

            //resources.ApplyResources(this.lbFrameFormat, this.lbFrameFormat.Name);
            //resources.ApplyResources(this.lbIDFormat, this.lbIDFormat.Name);
            //cbxFrameFormat.Items[0] = resources.GetString("cbxFrameFormat.Items");
            //cbxFrameFormat.Items[1] = resources.GetString("cbxFrameFormat.Items1");
            //cbxFrameFormat.Items[2] = resources.GetString("cbxFrameFormat.Items2");
            //cbxIDFormat.Items[0] = resources.GetString("cbxIDFormat.Items");
        }

        private Device GetSelectedDevice()
        {
            string deviceName = this.cbxSelectDevice.SelectedItem.ToString();
            if (deviceName == null || deviceName.Equals(string.Empty))
            {
                Logger.Info("no selected device.");
                return null;
            }

            Logger.Info(string.Format("selected device: [{0}]", deviceName));
            return (Device)pNameDevicePairs[deviceName];
        }

        private List<FormData> FindMappingFormDatas(Device device)
        {
            List<FormData> pMappingFormDatas = new List<FormData>();
            if (device == null)
            {
                return pFormDatas;
            }

            foreach (FormData pFormData in this.pFormDatas)
            {
                if (object.ReferenceEquals(pFormData.GetChannel().ParentDevice, device))
                {
                    pMappingFormDatas.Add(pFormData);
                }
            }
            return pMappingFormDatas;
        }

        #endregion

        #region form & controlers events

        private void FormMain_Load(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(Loading));
            thread.IsBackground = true;
            thread.Start();

            Init();
            InitLoadControls();
            thread.Join();

            // show add device dialog

            this.Activate();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Device device in pDeviceGroup.Devices)
            {
                device.CloseDevice();
            }

            foreach(FormData pFormData in pFormDatas)
            {
                pFormData.Close();
            }
            pFormStatus.Close();
            pFormDevice.Close();

            this.Dispose();
        }

        private void menuItemLanguage_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Logger.Info(string.Format("menuItemLanguage_DropDownItemClicked -> clickedItem: [{0}]", e.ClickedItem.Text));
            ToolStripMenuItem item = (ToolStripMenuItem)e.ClickedItem;
            if (item.Checked)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            SetLanguage((string)item.Tag);

            int oldIndex = Convert.ToInt32(menuItemLanguage.Tag);
            ToolStripMenuItem oldItem = (ToolStripMenuItem)menuItemLanguage.DropDownItems[oldIndex];
            oldItem.Checked = false;

            item.Checked = true;
            int newIndex = menuItemLanguage.DropDownItems.IndexOf(item);
            menuItemLanguage.Tag = newIndex;

            this.Cursor = Cursors.Default;
        }

        private void menuItemSkin_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Logger.Info(string.Format("menuItemSkin_DropDownItemClicked -> clickedItem: [{0}]", e.ClickedItem.Text));
            ToolStripMenuItem item = (ToolStripMenuItem)e.ClickedItem;
            if (item.Checked)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            string skinFile = item.Tag == null ? null : (string)item.Tag;
            skinEngine.SkinFile = skinFile;

            int oldIndex = Convert.ToInt32(menuItemSkin.Tag);
            ToolStripMenuItem oldItem = (ToolStripMenuItem)menuItemSkin.DropDownItems[oldIndex];
            oldItem.Checked = false;

            item.Checked = true;
            int newIndex = menuItemSkin.DropDownItems.IndexOf(item);
            menuItemSkin.Tag = newIndex;

            this.Cursor = Cursors.Default;
        }

        private void cbxSelectDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            Device oldSelectedDevice = this.pDeviceGroup.GetSelectedDevice();
            Device newSelectedDevice = GetSelectedDevice();
            this.pDeviceGroup.ChangeSelectedDevice(newSelectedDevice);
        }

        private void menuItemAbout_Click(object sender, EventArgs e)
        {
            new AboutUs().ShowDialog();
        }

        private void itemAddDevice_Click(object sender, EventArgs e)
        {
            DialogDevice pDialogDevice = new DialogDevice(null);
            if (pDialogDevice.ShowDialog() == DialogResult.OK)
            {
                this.pDeviceGroup.Add(pDialogDevice.GetDevice());
                pDialogDevice.Close();
            }
        }

        private void itemDeleteDevice_Click(object sender, EventArgs e)
        {
            Device device = this.pDeviceGroup.GetSelectedDevice();
            if (device == null)
            {
                MessageBox.Show("请选择要删除的设备.");
                return;
            }

            string deviceName = device.GetDeviceName();
            string content = string.Format("确定要删除当前设备[{0}]吗？", deviceName);

            DialogResult dialogResult = MessageBox.Show(content, "删除设备", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.OK)
            {
                this.pDeviceGroup.Remove(device);
            }
        }

        private void itemConfigDevice_Click(object sender, EventArgs e)
        {
            Device device = this.pDeviceGroup.GetSelectedDevice();
            if (device == null)
            {
                MessageBox.Show("请选择要配置的设备.");
                return;
            }
            DialogDevice pDialogDevice = new DialogDevice(device);
            if (pDialogDevice.ShowDialog() == DialogResult.OK)
            {
                this.pDeviceGroup.Update(pDialogDevice.GetDevice());
                pDialogDevice.Close();
            }
        }

        private void itemResetDevice_Click(object sender, EventArgs e)
        {
            Device device = this.pDeviceGroup.GetSelectedDevice();
            if (device == null)
            {
                MessageBox.Show("请选择要复位的设备.");
                return;
            }
            string deviceName = device.GetDeviceName();
            string content = string.Format("确定要复位当前设备[{0}]吗？", deviceName);
            DialogResult dialogResult = MessageBox.Show(content, "复位设备", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.OK)
            {
                foreach (Channel channel in device.Channels)
                {
                    channel.ResetCAN();
                }
                this.pDeviceGroup.Update(device);
            }
        }

        private void menuItemViewData_Click(object sender, EventArgs e)
        {
            foreach (FormData pFormData in this.pFormDatas)
            {
                pFormData.Visible = true;
                pFormData.Activate();
            }
        }

        private void menuItemViewDevice_Click(object sender, EventArgs e)
        {
            this.pFormDevice.Visible = true;
            this.pFormDevice.Activate();
        }

        private void menuItemViewStatus_Click(object sender, EventArgs e)
        {
            this.pFormStatus.Visible = true;
            this.pFormStatus.Activate();
        }

        #endregion

    }
}
