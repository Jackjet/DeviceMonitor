using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Mvc;
using DeviceMonitor.Dao;
using Newtonsoft.Json;
using WebGrease.Css.Extensions;

namespace DeviceMonitor.Models.DeviceModels
{
    public enum DataType
    {
        DeviceBit = 0,
        DeviceReal = 1
    }

    [Table("DeviceData")]
    [Serializable]
    //[Bind(Exclude = "value")]
    public class DeviceData:DeviceBase
    {
        /// <summary>
        /// �豸����
        /// </summary>
        [ForeignKey("parentId")]
        public virtual DeviceInfo DeviceInfo { get; set; }


        public Guid parentId { get; set; }

        /// <summary>
        /// �ڵ�״̬�������ڵ㻹��Ҷ�ӽڵ�
        /// </summary>
        public override string state
        {
            get { return "open"; }
            set { base.state = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public int groupIndex { get; set; }

        /// <summary>
        /// ��λ
        /// </summary>
        public string unit { get; set; }

        /// <summary>
        /// �豸��ַ
        /// </summary>
        public int address { get; set; }


        /// <summary>
        /// ������������λintʱ�����ֽڳ��ȣ������������λboolʱ����λ��
        /// </summary>
        public int lengthOrIndex{ get; set; }

        /// <summary>
        /// ���ñ���
        /// </summary>
        public bool AlarmAble { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public int? upper { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public int? lower { get; set; }

        /// <summary>
        /// �豸������
        /// </summary>
        public virtual int? value { get; set; }

        /// <summary>
        /// �豸��������
        /// </summary>
        public string group { get; set; }

        /// <summary>
        /// �豸����������
        /// </summary>
        public  DataType type { get; set; }

        public virtual ICollection<DeviceDataFormat> DeviceDataFormats { get; set; }

        [NotMapped]
        public override NodeType nodeType
        {
            get { return NodeType.Data; }
        }

        public override DeviceBase Clone()
        {
            var deviceBase = new DeviceData
            {
                id = Guid.NewGuid(),
                index = index,
                name = name,
                state = state,
                type = type,
                group =  group,
                lengthOrIndex = lengthOrIndex,
                address = address,
                groupIndex = groupIndex,
                upper = upper,
                lower = lower,
                unit = unit,
                AlarmAble = AlarmAble,
                DeviceDataFormats = new Collection<DeviceDataFormat>()
            };
            DeviceDataFormats.ForEach(m =>
            {
                var element = m.Clone() as DeviceDataFormat;
                if (element != null)
                {
                    element.parentId = deviceBase.id;
                    element.DeviceData = deviceBase;
                }
                deviceBase.DeviceDataFormats.Add(element);
            });
            return deviceBase;
        }
    }
}