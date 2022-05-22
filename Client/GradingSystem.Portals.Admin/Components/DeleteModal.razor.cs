using System.Text.Json;
using GradingSystem.Portals.Admin.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace GradingSystem.Portals.Admin.Components
{

    public partial class DeleteModal
    {

        [Parameter]
        public string Text { get; set; }
        
        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public EventCallback<bool> OnClose { get; set; }

        private Task ModalCancel()
        {
            return OnClose.InvokeAsync(false);
        }

        private Task ModalOk()
        {
            return OnClose.InvokeAsync(true);
        }
    }
}