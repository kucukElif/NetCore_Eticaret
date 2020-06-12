using DAL.Entity.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO.Pipes;
using System.Text;
using System.Security.Principal;
using System.Net;

namespace DAL.Entity.Base
{
    public class CoreEntity : IEntity<Guid>
    {
        public CoreEntity()
        {
            this.Status = Status.Active;
            this.CreatedDate = DateTime.Now;
            this.CreatedAdUserName = WindowsIdentity.GetCurrent().Name;
            this.CreatedComputerName = Environment.MachineName;
            //Todo: Ip alma işlemi gerçekleştirelecek.
            this.CreatedIP = GetHostName();
            this.CreatedBy = "admin";
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        public Status Status { get; set; }
        public DateTime? CreatedDate{ get; set; }
        public string CreatedComputerName { get; set; }
        public string CreatedIP { get; set; }
        public string CreatedAdUserName { get; set; }
        public string CreatedBy { get; set; }


        public DateTime? ModifiedDate { get; set; }
        public string ModifiedComputerName { get; set; }
        public string ModifiedIP { get; set; }
        public string ModifiedAdUserName { get; set; }
        public string ModifiedBy { get; set; }

        //Ip'e ulaşmak için tanımlandı
        public static string GetHostName() {
            string ip = null;
            IPAddress[] localIps = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (var item in localIps)
            {
                if (item.AddressFamily==System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ip=item.ToString();

                }
            }
            return ip;
        }
    }
}
