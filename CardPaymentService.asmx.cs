using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace BankService
{
    /// <summary>
    /// Summary description for CardPaymentService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CardPaymentService : System.Web.Services.WebService
    {
        TrainingExamplesEntities db = new TrainingExamplesEntities();
            
        [WebMethod]
        public string ProcessPayment(long cardno, int cvv, string expdate,decimal totamt)
        {
            Random r = new Random();
            try
            {
                var card = db.CardInfoes.Where(x => x.CardNo == cardno && x.cvvno == cvv && x.expdate == expdate).SingleOrDefault();
                if (card == null)
                    throw new Exception("Invalid Card Details");
                else
                {
                    if(card.balance >= totamt)
                    {
                        card.balance -= totamt;
                        var res=db.SaveChanges();
                        if (res > 0)
                        {
                            return "Transaction Number:" + r.Next() + ".Payment processed successfully";
                        }
                    }
                    else
                    {
                        throw new Exception("Insufficient Balance. Cannot process payment");
                    }
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
            return string.Empty;
        }
        public string HelloWorld()
        {
            return "Hello World";
        }
    }
}
