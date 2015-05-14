using System.Net;
using Common.Logging;
using System.Collections.Specialized;
using WebDAVSharp.Server;
using WebDAVSharp.Server.Adapters;
using WebDAVSharp.Server.Stores.DiskStore;

namespace WebDAVSharp.Example
{
    class Program
    {
        // IMPORTANT !!
        // change these variables to your wanted configuration
        private const string Localpath = @"c:\";
        private const string Url = "http://localhost:8880/";

        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            InitConsoleLogger();
            StartServer();
        }

        /// <summary>
        /// Initializes the console logger.
        /// </summary>
        private static void InitConsoleLogger()
        {
            // create properties
            NameValueCollection properties = new NameValueCollection();
            properties["showDateTime"] = "true";
            // set Adapter
            LogManager.Adapter =
                new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter(properties);
        }

        /// <summary>
        /// Starts the server.
        /// Authentication used: Negotiate
        /// </summary>
        private static void StartServer()
        {
            WebDavServer server = new WebDavServer(new WebDavDiskStore(Localpath));
            server.Start(Url);
        }
    }
}
