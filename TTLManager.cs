using Microsoft.Win32;

namespace TTLChanger
{
    public class TTLManager
    {
        private RegistryKey _tcpipKey;
        private RegistryKey _tcpip6Key;
        private string _defaultTTLPropertyName;

        private string _v4TTL;
        private string _v6TTL;

        public TTLManager()
        {
            _defaultTTLPropertyName = "DefaultTTL";

            _tcpipKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters", true);
            _tcpip6Key = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\Tcpip6\\Parameters", true);

            _v4TTL = GetDefaultTTLValue(_tcpipKey);
            _v6TTL = GetDefaultTTLValue(_tcpip6Key);
        }

        public string V4TTL
        {
            get => _v4TTL;
            set => _tcpipKey.SetValue(_defaultTTLPropertyName, value, RegistryValueKind.DWord);
        }

        public string V6TTL
        {
            get => _v6TTL;
            set => _tcpip6Key.SetValue(_defaultTTLPropertyName, value, RegistryValueKind.DWord);
        }

        private bool HaveDefaultTTLParameter(RegistryKey key)
        {
            object property = key.GetValue(_defaultTTLPropertyName);
            return true ? property != null : false;
        }
    
        private string GetDefaultTTLValue(RegistryKey key)
        {
            return HaveDefaultTTLParameter(key) ? key.GetValue(_defaultTTLPropertyName).ToString() : "-1";
        }
    }
}
