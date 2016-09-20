//
// The information contained herein is property of Nordic Semiconductor ASA.
// Terms and conditions of usage are described in detail in NORDIC
// SEMICONDUCTOR STANDARD SOFTWARE LICENSE AGREEMENT. 
//
// Licensees are granted free, non-transferable use of the information. NO
// WARRENTY of ANY KIND is provided. This heading must NOT be removed from
// the file.
//
// $LastChangedRevision: 2114 $
//
// @author Ole Saether
//
using System;
using System.IO;

namespace Lu1BootLdrTest
{
	/// <summary>
	/// Summary description for HexFile.
	/// </summary>
    public class HexFile
    {
        private string fileName_;

        public HexFile(String fileName)
        {
            fileName_ = fileName;
        }

        public bool Write(byte[] pBuf, uint maxAddr)
        {
            using (StreamWriter sw = new StreamWriter(fileName_))
            {
                uint crc;
                uint addr = 0, a, ta;
                while(maxAddr > addr)
                {
                    if ((maxAddr-addr) >= 15)
                    {
                        ta = 16;
                    }
                    else
                    {
                        ta = maxAddr - addr + 1;
                    }                        
                    sw.Write(":");
                    crc = ta & 0x0ff;
                    sw.Write(ta.ToString("X2"));
                    crc += addr & 0x0ff;
                    crc += (addr >> 8) & 0xff;
                    sw.Write(addr.ToString("X4"));
                    sw.Write("00");
                    for(a=0;a<ta;a++)
                    {
                        crc += pBuf[addr+a];
                        sw.Write(pBuf[addr+a].ToString("X2"));
                    }
                    crc = (~crc + 1) & 0xff;
                    sw.WriteLine(crc.ToString("X2"));
                    addr += ta;
                }
                sw.WriteLine(":00000001FF");
            }            
            return true;
        }

        public bool Read(byte[] pBuf, int nOffset, ref uint maxAddr)
        {
            using (StreamReader sr = new StreamReader(fileName_))
            {
                String line;
                maxAddr = 0;
                while ((line = sr.ReadLine()) != null) 
                {
                    ProcessLine(line, pBuf, nOffset, ref maxAddr);
                }
            }
            return true;
        }

        private bool ProcessLine(String line, byte[] pBuf, int nOffset, ref uint maxAddr)
        {
            line.Trim();
            if (!line.StartsWith(":"))
            {
                throw(new ApplicationException("The specified file is not a valid Intel hex-file"));
            }
            uint nBytes = Convert.ToUInt32(line.Substring(1, 2), 16);
            uint nAddr = Convert.ToUInt32(line.Substring(3, 4), 16);
            uint nType = Convert.ToUInt32(line.Substring(7, 2), 16);
            uint nCrc = (nBytes & 0xff) + (nAddr & 0xff) + ((nAddr >> 8) & 0xff) + (nType & 0xff);
            int nPos = 0;
            switch(nType)
            {
                case 0: // Data record:
                    for (nPos=0;nPos<nBytes;nPos++)
                    {
                        byte b = Convert.ToByte(line.Substring(9+2*nPos, 2), 16);
                        if (nAddr >= pBuf.Length)
                        {
                            throw(new ApplicationException("The contents of the Intel hex-file does not fit into the supplied buffer"));
                        }
                        pBuf[nAddr+nOffset] = b;
                        maxAddr = Math.Max(nAddr+(uint)nOffset, maxAddr);
                        nCrc += b;
                        nAddr++;
                    }
                    break;

                case 1: // End record
                    break;

                case 2: // Extended address record:
                    throw(new ApplicationException("Extended address records not supported"));

                case 3: // Ignore record type 3 (program start address?)
                    for (nPos=0;nPos<nBytes;nPos++)
                    {
                        nCrc += Convert.ToByte(line.Substring(9+2*nPos, 2), 16);
                    }
                    break;

                default:
                    throw(new ApplicationException("Unsupported hex-file format"));
            }
            nCrc = (~nCrc + 1) & 0xff;
            if (Convert.ToUInt32(line.Substring(9+2*nPos, 2), 16) != nCrc)
                throw(new ApplicationException("CRC error in hex-file"));
            return true;
        }
    }
}


