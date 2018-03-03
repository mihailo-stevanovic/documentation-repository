using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocRepoApi.Models
{
    /// <summary>
    /// High level document categorization.
    /// </summary>
    public enum DocumentCategory
    {
        FunctionalDocumentation, TechnicalDocumentation, ReleaseNotes, Other
    }
}
