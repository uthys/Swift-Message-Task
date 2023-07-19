using System.Data.SqlTypes;
using WebApplication1.Models;

namespace WebApplication1.Service
{
    public class Formating
    {
        public SwiftMessage FormatToSwiftMessage(string swiftMessage)
        {
            int start;
            int end;
            string substring;
            SwiftMessage message = new SwiftMessage();

            start = swiftMessage.IndexOf("{1:");
            end = swiftMessage.IndexOf("}", start);
            if (start != -1 && end != -1)
            {
                substring = swiftMessage.Substring(start + 3, end - start - 3);
                message.Field1 = substring;
            }

            start = swiftMessage.IndexOf("{2:");
            end = swiftMessage.IndexOf("}", start);
            if (start != -1 && end != -1)
            {
                substring = swiftMessage.Substring(start + 3, end - start - 3);
                message.Field2 = substring;
            }

            start = swiftMessage.IndexOf(":20:");
            end = swiftMessage.IndexOf(":21:", start);
            if (start != -1 && end != -1)
            {
                string field20Value = swiftMessage.Substring(start + 4, end - start - 4);

                start = end;
                end = swiftMessage.IndexOf(":79:", start);
                if (start != -1 && end != -1)
                {
                    string field21Value = swiftMessage.Substring(start + 4, end - start - 4);

                    start = end;
                    end = swiftMessage.IndexOf("}", start);
                    if (start != -1 && end != -1)
                    {
                        string field79Value = swiftMessage.Substring(start + 4, end - start - 4);

                        SwiftMessageContent content = new SwiftMessageContent
                        {
                            Field20 = field20Value,
                            Field21 = field21Value,
                            Field79 = field79Value
                        };

                        message.Field4 = content;
                    }
                }
            }

            start = swiftMessage.IndexOf("{5:{MAC:");
            end = swiftMessage.IndexOf("}", start);
            if (start != -1 && end != -1)
            {
                string macValue = swiftMessage.Substring(start + 7, end - start - 7);

                start = swiftMessage.LastIndexOf("{CHK:");
                end = swiftMessage.IndexOf("}", start);
                if (start != -1 && end != -1)
                {
                    string chkValue = swiftMessage.Substring(start + 5, end - start - 5);
                    SwiftMessageChecksum checksum = new SwiftMessageChecksum
                    {
                        MAC = macValue,
                        CHK = chkValue
                    };

                    message.Field5 = checksum;
                }
            }
            return message;
        }
    }
}
