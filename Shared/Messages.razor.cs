using Microsoft.AspNetCore.Components;

namespace EkaToolFusion.Shared
{
    public partial class Messages : ComponentBase
    {
        private const string ERROR_BG_COLOR = "#D50000";
        private const string SUCCESS_BG_COLOR = "#09af00";
        private const string WARNING_BG_COLOR = "#f47100";

        [Parameter]
        public List<MessageTemplate> MessagesTemplates { get; set; } = new List<MessageTemplate>();

        public void ShowError(string message)
            => ShowMessage(message, ERROR_BG_COLOR);            

        public void ShowSuccess(string message)
            => ShowMessage(message, SUCCESS_BG_COLOR);
        
        public void ShowWarning(string message)
            => ShowMessage(message, WARNING_BG_COLOR);

        private void ShowMessage(string message, string bg_color)
        {
            var messageTemplate = new MessageTemplate(message, bg_color);
            MessagesTemplates.Add(messageTemplate);
            StateHasChanged();
            _ = Task.Delay(5000)
                .ContinueWith(r => InvokeAsync(() => RemoveMessage(messageTemplate.MessageID)));
        }

        private string RemoveMessage(string messageID)
        {
            var message = MessagesTemplates.Where(x => x.MessageID == messageID).FirstOrDefault();
            
            if(message != null)
            {
                MessagesTemplates.Remove(message);
            }

            StateHasChanged();

            return messageID;
        }
    }

    public class MessageTemplate(string message, string bgColor)
    {
        public string MessageID { get; set; } = Guid.NewGuid().ToString();
        public string BackgroundColor { get; set; } = bgColor;
        public string Message { get; set; } = message;
        public long CreatedAt { get; set; } = DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }
}