using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ReferralCodeGenerator
{
    class ReferralCodeGeneratorTests
    {
        [Test]
        public void BatchTestReferralCode()
        {
            CodeGenerator referralC = new CodeGenerator();

            for (int i = 0; i < 1000000; i++)
            {

                var code = referralC.GetCodeFromId(i);

                var customerid = referralC.GetCustomerIdFromCode(code);

                if (customerid != i)
                {
                    Assert.Fail("Not Equal");
                }           
            }
            Assert.Pass();
        }
    }
}