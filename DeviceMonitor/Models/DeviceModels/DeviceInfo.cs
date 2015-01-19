using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using DeviceMonitor.Dao;
using Newtonsoft.Json;
using WebGrease.Css.Extensions;

namespace DeviceMonitor.Models.DeviceModels
{
    [Table("DeviceInfo")]
    [Serializable]
    public class DeviceInfo : DeviceBase
    {
        
        /// <summary>
        /// �����豸��
        /// </summary>
        [ForeignKey("parentId")]
        public virtual DeviceGroup DeviceGroup { get; set; }

        public Guid parentId { get; set; }

        /// <summary>
        /// �豸IP��ַ
        /// </summary>
        public string ip { get; set; }

        /// <summary>
        /// �豸IP��ַ
        /// </summary>
        public int port { get; set; }

        /// <summary>
        /// ����ͷ����
        /// </summary>
        public int headerLength { get; set; }

        /// <summary>
        /// ���ݰ�����
        /// </summary>
        public int dataLength { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public byte commandByte { get; set; }

        public override string state
        {
            get
            {
                return "closed";
            }
            set { base.state = value; }
        }

        public virtual ICollection<DeviceData> DeviceDatas { get; set; }

        [NotMapped]
        public override NodeType nodeType
        {
            get { return NodeType.Info; }
        }

        public override DeviceBase Clone()
        {
            var deviceBase = new DeviceInfo
            {
                id = Guid.NewGuid(),
                commandByte = commandByte,
                headerLength = headerLength,
                index = index,
                dataLength = dataLength,
                ip = ip,
                name = name,
                state = state,
                port = port,
                DeviceDatas = new Collection<DeviceData>()
            };
            DeviceDatas.ForEach(m =>
            {
                var element = m.Clone() as DeviceData;
                if (element != null)
                {
                    element.parentId = deviceBase.id;
                    element.DeviceInfo = deviceBase;
                }
                deviceBase.DeviceDatas.Add(element);
            });
            return deviceBase;
        }
    }
}