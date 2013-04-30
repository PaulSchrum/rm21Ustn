using Bentley.Interop.MicroStationDGN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rm21Ustn.rm2Uelement
{
   public class rm2UtextNode : rm2UtextBase
   {
      public rm2UtextNode() : base() { }

      public rm2UtextNode(Element el) : base(el) { }

      public TextNodeElement EL
      {
         get { return (TextNodeElement)el; }
         private set { }
      }

      public override String getTextValue()
      {
         var textNode = (el as TextNodeElement);
         StringBuilder returnList = new StringBuilder();

         int i;
         for (i = 0; i < textNode.TextLinesCount; i++)
         {
            if (i > 0)
               returnList.Append(System.Environment.NewLine);

            returnList.Append (textNode.get_TextLine(i));
         }

         return returnList.ToString();
      }
   }
}
