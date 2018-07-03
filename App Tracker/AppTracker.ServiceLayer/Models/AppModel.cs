namespace AppTracker.ServiceLayer.Models
{
    public class AppModel
    {
        public int Id { get; set; } // ID (Primary key)
        public string Name { get; set; } // Name (length: 300)
        public string Description { get; set; } // Description (length: 500)
        public string ProductionUrl { get; set; } // ProductionURL (length: 300)
        public string FriendlyUrl { get; set; } // FriendlyURL (length: 300)
        public string TestUrl { get; set; } // TestURL (length: 300)
        public string Database { get; set; } // Database (length: 300)
        public string DatabaseVersion { get; set; } // DatabaseVersion (length: 100)
        public string DatabaseInstance { get; set; } // DatabaseInstance (length: 300)
        public string TestDatabase { get; set; } // TestDatabase (length: 300)
        public string WebServer { get; set; } // WebServer (length: 300)
        public string MvcVersion { get; set; } // MVCVersion (length: 100)
        public string NetVersion { get; set; } // NETVersion (length: 100)
        public string AppPoolName { get; set; } // AppPoolName (length: 100)
        public string FilePath { get; set; } // FilePath (length: 300)
        public string  TfsPath { get; set; } // TFSPath (length: 300)
        public System.DateTime? EndDateTime { get; set; } // EndDateTime
        public string DevIp { get; set; } // DevIP (length: 500)
        public string ProdIp { get; set; } // ProdIP (length: 500)
    }
}
