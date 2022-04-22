//Copyright (c) Microsoft Corporation.  All rights reserved.

using System.Collections.ObjectModel;
using System.Text;

namespace Kamishibai
{
    /// <summary>Stores the file extensions used when filtering files in File Open and File Save dialogs.</summary>
    public class FileDialogFilter
    {
        // We'll keep a parsed list of separate extensions and rebuild as needed.

        private readonly List<string> _extensions = new();

        /// <summary>Creates a new instance of this class with the specified display name and file extension list.</summary>
        /// <param name="rawDisplayName">The name of this filter.</param>
        /// <param name="extensions">The array of extensions in this filter. See remarks.</param>
        /// <remarks>
        /// The <paramref name="extensions"/> Extensions can be prefaced with a period (".") or with the file wild card specifier "*.".
        /// </remarks>
        /// <permission cref="System.ArgumentNullException">The <paramref name="extensions"/> cannot be null or a zero-length string.</permission>
        public FileDialogFilter(string rawDisplayName, params string[] extensions)
        {
            RawDisplayName = rawDisplayName;

            // Parse string and create extension strings.
            // Format: "bat,cmd", or "bat;cmd", or "*.bat;*.cmd" Can support leading "." or "*." - these will be stripped.
            foreach (var extension in extensions)
            {
                _extensions.Add(NormalizeExtension(extension));
            }
        }

        public string RawDisplayName { get; }
        /// <summary>Gets or sets the display name for this filter.</summary>
        /// <permission cref="System.ArgumentNullException">The value for this property cannot be set to null or a zero-length string.</permission>
        public string DisplayName =>
            string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "{0} ({1})",
                RawDisplayName,
                GetDisplayExtensionList(_extensions));

        /// <summary>Gets a collection of the individual extensions described by this filter.</summary>
        public IReadOnlyList<string> Extensions => _extensions;

        /// <summary>Returns a string representation for this filter that includes the display name and the list of extensions.</summary>
        /// <returns>A <see cref="System.String"/>.</returns>
        public override string ToString() => DisplayName;

        private static string GetDisplayExtensionList(List<string> extensions)
        {
            var extensionList = new StringBuilder();
            foreach (var extension in extensions)
            {
                if (extensionList.Length > 0) { extensionList.Append(", "); }
                extensionList.Append("*.");
                extensionList.Append(extension);
            }

            return extensionList.ToString();
        }

        private static string NormalizeExtension(string rawExtension)
        {
            rawExtension = rawExtension.Trim();
            rawExtension = rawExtension.Replace("*.", null);

            //remove only the first dot so multi-dotted extensions work
            int indexOfDot = rawExtension.IndexOf('.');
            if (indexOfDot != -1)
            {
                rawExtension.Remove(indexOfDot);
            }

            return rawExtension;
        }
    }
}