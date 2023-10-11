using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace InfiniReplay.Installers
{
    internal class MenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            Plugin.Log.Info("MenuInstaller InstallBindings");
            Container.Bind<ReplayLoader.ReplayService>().AsSingle();
            Container.BindInterfacesTo<MenuHandler>().AsSingle();
            Plugin.Log.Notice("MenuInstaller InstallBindings DONE");
        }
    }
}
