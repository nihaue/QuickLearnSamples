using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;

namespace QuickLearn.BizTalk.Components
{
    internal static class Utility
    {
        internal static IBaseMessage CloneMessage(IBaseMessage inmsg, IPipelineContext pc)
        {
            
            IBaseMessageFactory messageFactory = pc.GetMessageFactory();

            var outmsg = messageFactory.CreateMessage();
            outmsg.Context = PipelineUtil.CloneMessageContext(inmsg.Context);

            // Generate new empty message body part, we will retain nothing from the old
            IBaseMessagePart body = messageFactory.CreateMessagePart();

            if ((inmsg != null) && (inmsg.BodyPart != null))
                body.PartProperties = PipelineUtil.CopyPropertyBag(inmsg.BodyPart.PartProperties, messageFactory);
            
            // This is what the XmlWriter will end up generating, and what appears in the
            // directive at the top of the file
            body.Charset = "UTF-8";
            body.ContentType = "text/xml";
            body.Data = null;
            
            CloneParts(pc, inmsg, outmsg, body);

            return outmsg;
        }

        private static void CloneParts(IPipelineContext pc, IBaseMessage inmsg, IBaseMessage outmsg, IBaseMessagePart bodyPart)
        {
            for (int i = 0; i < inmsg.PartCount; i++)
            {
                string partName = null;

                IBaseMessagePart currentPart = inmsg.GetPartByIndex(i, out partName);

                if (currentPart == null) continue;

                outmsg.AddPart(partName, partName == inmsg.BodyPartName ? bodyPart : currentPart, partName == inmsg.BodyPartName);
            }
        }
    }
}