//------------------------------------------------------------------------------
// <copyright file="XmlCodeExporter.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// <owner current="true" primary="true">Microsoft</owner>                                                                
//------------------------------------------------------------------------------

namespace System.Xml.Serialization
{
    using System.CodeDom;

    //
    // Summary:
    //     Represents a class that can generate proxy code from an XML representation of
    //     a data structure.
    public abstract class CodeExporter : System.Object
    {
        //
        // Summary:
        //     Gets a collection of code attribute metadata that is included when the code is
        //     exported.
        //
        // Returns:
        //     A collection of System.CodeDom.CodeAttributeDeclaration objects that represent
        //     metadata that is included when the code is exported.
        public CodeAttributeDeclarationCollection IncludeMetadata { get; }
    }
}
