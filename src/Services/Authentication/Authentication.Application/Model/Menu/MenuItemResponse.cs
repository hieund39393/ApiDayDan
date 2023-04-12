using System;
using System.Collections.Generic;

namespace Authentication.Application.Model.Menu
{
    public class MenuItemResponse
    {
        public MenuItemResponse() { }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public int Order { get; set; }
        public List<MenuItemResponse> SubItems { get; set; }
    }
}
