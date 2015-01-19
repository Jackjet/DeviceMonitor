using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;

namespace DeviceMonitor.Models.DeviceModels
{
    public enum ChangeFormat
    {
        ReplaceWithStr = 0,
        PlusWith = 1,
        MultiWithStr = 2,
        DividWith = 3
    }
    [Table("DeviceDataFormat")]
    [Serializable]
    public class DeviceDataFormat:DeviceBase
    {
        [ForeignKey("parentId")]
        public virtual DeviceData DeviceData { get; set; }

        public Guid parentId { get; set; }

        /// <summary>
        /// ��ʽ����ʽ
        /// </summary>
        public ChangeFormat changeFormat { get; set; }

        /// <summary>
        /// ��ʽ����
        /// </summary>
        public int key { get; set; }
        

        /// <summary>
        /// ��ʽ��Ŀ���ַ���
        /// </summary>
        public string value { get; set; }

        [NotMapped]
        public override NodeType nodeType
        {
            get { return NodeType.Format; }
        }

        public override DeviceBase Clone()
        {
            var deviceBase = new DeviceDataFormat
            {
                id = Guid.NewGuid(),
                index = index,
                name = name,
                state = state,
                value = value,
                key = key,
                changeFormat = changeFormat
            };
            return deviceBase;
        }
    }
}