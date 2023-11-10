using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace Berke.Marcas.WebUI.Helpers
{
    public class ImpersonateException : Exception
    {
        public ImpersonateException(string Message) : base(Message)
        {

        }
    }

    public class Impersonate : IDisposable
    {
        private const int LOGON32_LOGON_INTERACTIVE = 2;
        private const int LOGON32_PROVIDER_DEFAULT = 0;

        #region windows API calls
         [DllImport("advapi32.dll")]
         private static extern int LogonUserA(String lpszUserName,
             String lpszDomain,
             String lpszPassword,
             int dwLogonType,
             int dwLogonProvider,
             ref IntPtr phToken);
         [DllImport("advapi32.dll", CharSet=CharSet.Auto, SetLastError=true)]
         private static extern int DuplicateToken(IntPtr hToken,
             int impersonationLevel,
             ref IntPtr hNewToken);
 
         [DllImport("advapi32.dll", CharSet=CharSet.Auto, SetLastError=true)]
         private static extern bool RevertToSelf();
 
         [DllImport("kernel32.dll", CharSet=CharSet.Auto)]
         private static extern  bool CloseHandle(IntPtr handle);
         #endregion

        readonly WindowsImpersonationContext impersonationContext;

        public Impersonate(string userName, string domain, string password)
        {
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;
            // firstly force any current Impersonation to revert to the default security context  
            /*if(RevertToSelf())
            {*/
                // Call the LogonUserA to authenticate the user and get the users token
                if(LogonUserA(userName, domain, password, LOGON32_LOGON_INTERACTIVE,
                    LOGON32_PROVIDER_DEFAULT, ref token) != 0)
                {

                    //The DuplicateToken function creates an impersonation token which can be used when creating a new windows identity 
                    if(DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                    {
                        WindowsIdentity tempWindowsIdentity;
                        tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                        impersonationContext = tempWindowsIdentity.Impersonate();
                        // we have the impersonationContext so we can close the handles to the tokens
                        if (impersonationContext != null)
                        {
                            CloseHandle(token);
                            CloseHandle(tokenDuplicate);
                        }
                    }
                    else
                    {
                        if (token != IntPtr.Zero)
                            CloseHandle(token);
                        if(tokenDuplicate!=IntPtr.Zero)
                            CloseHandle(tokenDuplicate);
                        throw new ImpersonateException(string.Format("Impersonation Failed - DuplicateToken ({0})", userName));
                    }
                }
                else
                {
                    if(token!= IntPtr.Zero)
                        CloseHandle(token);
                    throw new ImpersonateException(string.Format("Impersonation Failed - LogonUserA ({0})", userName));
                }
            /*}
            else
            {
                throw new ImpersonateException("Impersonation Failed - RevertToSelf");
            }*/
        }

        public void Dispose()
        {       
            impersonationContext.Undo();
        }
    }
}