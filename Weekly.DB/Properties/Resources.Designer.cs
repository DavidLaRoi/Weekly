//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Weekly.DB.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Weekly.DB.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to dotnet ef dbcontext scaffold &quot;Server=DESKTOP-V2N2JBV\SQLEXPRESS;Database=Weekly;Trusted_Connection=True;&quot; Microsoft.EntityFrameworkCore.SqlServer -o Models --project C:\Users\DavidLaRoiLogic4\Repos\Weekly\Weekly.DB\Weekly.DB.csproj -f -c &quot;WeeklyContextBase&quot; --no-onconfiguring.
        /// </summary>
        internal static string fro_mcommandline {
            get {
                return ResourceManager.GetString("fro mcommandline", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Scaffold-DbContext &quot;Server=DESKTOP-V2N2JBV\SQLEXPRESS;Database=Weekly;Trusted_Connection=True;&quot; &quot;Microsoft.EntityFrameworkCore.SqlServer&quot; -OutputDir &quot;Models&quot; -Context &quot;WeeklyContextBase&quot; -Force.
        /// </summary>
        internal static string Scaffold_string {
            get {
                return ResourceManager.GetString("Scaffold string", resourceCulture);
            }
        }
    }
}
