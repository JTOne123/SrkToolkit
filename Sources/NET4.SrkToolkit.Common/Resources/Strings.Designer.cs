﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18449
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SrkToolkit.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SrkToolkit.Resources.Strings", typeof(Strings).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The field {0} must be lower than {1}..
        /// </summary>
        public static string DateRangeAttribute_ErrorMessage_Max {
            get {
                return ResourceManager.GetString("DateRangeAttribute_ErrorMessage_Max", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The field {0} must be greater than {1}..
        /// </summary>
        public static string DateRangeAttribute_ErrorMessage_Min {
            get {
                return ResourceManager.GetString("DateRangeAttribute_ErrorMessage_Min", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The field {0} must be between {1} and {2}..
        /// </summary>
        public static string DateRangeAttribute_ErrorMessage_MinMax {
            get {
                return ResourceManager.GetString("DateRangeAttribute_ErrorMessage_MinMax", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The field {0} is not valid..
        /// </summary>
        public static string DateRangeAttribute_ErrorMessage_WTF {
            get {
                return ResourceManager.GetString("DateRangeAttribute_ErrorMessage_WTF", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A valid email address is required..
        /// </summary>
        public static string EmailAddressAttribute_ErrorMessage {
            get {
                return ResourceManager.GetString("EmailAddressAttribute_ErrorMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There are {0} email addresses; a maximum of {1} can be accepted..
        /// </summary>
        public static string EmailAddressAttribute_ErrorMessage_MultipleMaximumAddresses {
            get {
                return ResourceManager.GetString("EmailAddressAttribute_ErrorMessage_MultipleMaximumAddresses", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There are only {0} email addresses; a minimum of {1} are expected..
        /// </summary>
        public static string EmailAddressAttribute_ErrorMessage_MultipleMinimumAddresses {
            get {
                return ResourceManager.GetString("EmailAddressAttribute_ErrorMessage_MultipleMinimumAddresses", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The timezone is not valid..
        /// </summary>
        public static string TimezoneAttribute_ErrorMessage {
            get {
                return ResourceManager.GetString("TimezoneAttribute_ErrorMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Both ErrorMessageResourceType and ErrorMessageResourceName need to be set on this attribute..
        /// </summary>
        public static string ValidationAttribute_NeedBothResourceTypeAndResourceName {
            get {
                return ResourceManager.GetString("ValidationAttribute_NeedBothResourceTypeAndResourceName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The property &apos;{0}&apos; on resource type &apos;{1}&apos; is not a string type..
        /// </summary>
        public static string ValidationAttribute_ResourcePropertyNotStringType {
            get {
                return ResourceManager.GetString("ValidationAttribute_ResourcePropertyNotStringType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The resource type &apos;{0}&apos; does not have an accessible static property named &apos;{1}&apos;..
        /// </summary>
        public static string ValidationAttribute_ResourceTypeDoesNotHaveProperty {
            get {
                return ResourceManager.GetString("ValidationAttribute_ResourceTypeDoesNotHaveProperty", resourceCulture);
            }
        }
    }
}
