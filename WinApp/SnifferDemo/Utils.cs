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
namespace Lu1BootLdrTest
{
    public class Utils
    {
        public static bool IsFlashBufEmpty(byte[] flashBuf)
        {
            for (uint i = 0; i < flashBuf.GetLength(0); i++)
            {
                if (flashBuf[i] != 0xff)
                    return false;
            }
            return true;
        }

        public static bool IsFlashBufEmpty(byte[] flashBuf, uint offset, uint count)
        {
            for (uint i = 0; i < count; i++)
            {
                if (flashBuf[offset + i] != 0xff)
                    return false;
            }
            return true;
        }

        // There must be a .NET way of doing this?
        public static void FillByteBuf(byte[] buf, byte val)
        {
            for (int i = 0; i < buf.Length; i++)
            {
                buf[i] = val;
            }
        }
    }
}
