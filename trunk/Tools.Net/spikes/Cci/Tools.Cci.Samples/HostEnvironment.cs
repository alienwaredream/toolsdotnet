using System;
using Microsoft.Cci;

namespace Tools.Net.Cci.Samples
{
    internal class HostEnvironment : MetadataReaderHost
    {
        PeReader peReader;
        internal HostEnvironment()
        {
            this.peReader = new PeReader(this);
        }
        public override IUnit LoadUnitFrom(string location)
        {
            return this.peReader.OpenModule(BinaryDocument.GetBinaryDocumentForFile(location, this));

        }
    }
}