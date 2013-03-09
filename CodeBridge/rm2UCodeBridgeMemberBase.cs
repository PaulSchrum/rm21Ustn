using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BCOM = Bentley.Interop.MicroStationDGN;
using rm21Ustn.rm2Uelement;

namespace rm21Ustn.CodeBridge
{
   internal class rm2UCodeBridgeMemberBase
   {
      public String Name { get; set; }
      public rm2UNamedGroup NamedGroup { get; set; }
   }
}
