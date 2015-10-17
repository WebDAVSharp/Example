using System.ServiceProcess;
using Common.Logging;
using WebDAVSharp.FileExample.Framework;
using WebDAVSharp.Server;
using WebDAVSharp.Server.Stores;
using WebDAVSharp.Server.Stores.DiskStore;
using WebDAVSharp.Server.Stores.Locks;
using WebDAVSharp.Server.Stores.Locks.Interfaces;
#if DEBUG
using Common.Logging;
using Common.Logging.Configuration;
using Common.Logging.Simple;

#endif

namespace WebDAVSharp.FileExample
{
    /// <summary>
    ///     The actual implementation of the windows service goes here...
    /// </summary>
    [WindowsService("WebDavSharp.FileExample",
        DisplayName = "WebDavSharp.FileExample",
        Description = "WebDavSharp.FileExample",
        EventLogSource = "WebDavSharp.FileExample",
        StartMode = ServiceStartMode.Automatic)]
    public class ServiceImplementation : IWindowsService
    {
        // IMPORTANT !!
        // change these variables to your wanted configuration
        private const string Localpath = @"c:\";
        private const string Url = "http://localhost:8880/";

        public void Dispose()
        {
        }

        /// <summary>
        ///     This method is called when the service gets a request to start.
        /// </summary>
        /// <param name="args">Any command line arguments</param>
        public void OnStart(string[] args)
        {
            InitConsoleLogger();
            StartServer();
        }

        /// <summary>
        ///     This method is called when the service gets a request to stop.
        /// </summary>
        public void OnStop()
        {
        }

        /// <summary>
        ///     This method is called when a service gets a request to pause,
        ///     but not stop completely.
        /// </summary>
        public void OnPause()
        {
        }

        /// <summary>
        ///     This method is called when a service gets a request to resume
        ///     after a pause is issued.
        /// </summary>
        public void OnContinue()
        {
        }

        /// <summary>
        ///     This method is called when the machine the service is running on
        ///     is being shutdown.
        /// </summary>
        public void OnShutdown()
        {
        }
        private static void InitConsoleLogger()
        {
#if DEBUG
            // create properties
            NameValueCollection properties = new NameValueCollection();
            properties["showDateTime"] = "true";
            // set Adapter
            LogManager.Adapter =
                new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter(properties);
#endif
        }

        /// <summary>
        /// Starts the server.
        /// Authentication used: Negotiate
        /// </summary>
        private static void StartServer()
        {
            IWebDavStoreItemLock lockSystem = new WebDavStoreItemLock();
            IWebDavStore store = new WebDavDiskStore(Localpath, lockSystem);
            WebDavServer server = new WebDavServer(ref store);
            server.Start(Url);
        }
    }
}