namespace Lu1BootLdrTest
{
    public enum USB_COMMANDS : byte
    {
        CMD_FIRMWARE_VERSION = 1,
        CMD_FLASH_WRITE_INIT,       // Eigth 64 bytes bulk packets <- PC follow after this command
        CMD_FLASH_READ,
        CMD_FLASH_ERASE_PAGE,
        CMD_FLASH_SET_PROTECTED,
        CMD_FLASH_SELECT_HALF,
        CMD_TEMP_RESET
    };
}
