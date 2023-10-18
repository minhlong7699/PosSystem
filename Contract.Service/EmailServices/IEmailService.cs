using Entity.Models.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Service.EmailServices
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
