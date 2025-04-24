using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_List.Services
{
    public interface ISettingsService
    {
        bool GetThemeSetting();
        void SaveThemeSetting(bool isDarkTheme);
    }
}
