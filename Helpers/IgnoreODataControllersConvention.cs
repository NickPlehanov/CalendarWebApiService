using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Web.Http.OData;

namespace CalendarWebApiService.Helpers
{
    public class IgnoreODataControllersConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (controller.ControllerType.IsDefined(typeof(ODataRoutingAttribute), inherit: false))
            {
                controller.ApiExplorer.IsVisible = false;
            }
        }
    }
}
