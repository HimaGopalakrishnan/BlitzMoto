﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace App.Resx {
    using System;
    using System.Reflection;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class AppResources {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AppResources() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("App.Resx.AppResources", typeof(AppResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        public static string Loading {
            get {
                return ResourceManager.GetString("Loading", resourceCulture);
            }
        }
        
        public static string Alert {
            get {
                return ResourceManager.GetString("Alert", resourceCulture);
            }
        }
        
        public static string Ok {
            get {
                return ResourceManager.GetString("Ok", resourceCulture);
            }
        }
        
        public static string Cancel {
            get {
                return ResourceManager.GetString("Cancel", resourceCulture);
            }
        }
        
        public static string Address {
            get {
                return ResourceManager.GetString("Address", resourceCulture);
            }
        }
        
        public static string Location {
            get {
                return ResourceManager.GetString("Location", resourceCulture);
            }
        }
        
        public static string Latitude {
            get {
                return ResourceManager.GetString("Latitude", resourceCulture);
            }
        }
        
        public static string Longitude {
            get {
                return ResourceManager.GetString("Longitude", resourceCulture);
            }
        }
    }
}
