using Microsoft.Management.Infrastructure;
using NextUnit.Core.Asserts;
using NextUnit.Core.TestAttributes;
using NextUnit.HardwareContext.SystemInformation;
using System.Security;
using System.ServiceProcess;

namespace NextUnit.HardwareContext.Tests
{
    /// <summary>
    /// Tests the hardware context.
    /// </summary>
    public class HardwareContextTests
    {
        [Test]
        [Group(nameof(SysInfo))]
        public void AllKnownFoldersTest()
        {
        }

        [Test]
        [Group(nameof(SysInfo))]
        public void AllocationGranularity()
        {
            //Assert.AreEqual<uint>(AllocationGranularity, SysInfo.AllocationGranularity);
        }

        [Test]
        [Group(nameof(SysInfo))]
        public void BankLabelsTest()
        {

        }

        [Test]
        [Group(nameof(SysInfo))]
        public void CapacityTest()
        {
            //Assert.AreEqual<ulong>(Capacity, SysInfo.Capacity);
        }

        [Test]
        [Group(nameof(SysInfo))]
        public void StackStraceTest()
        {
            Assert.AreEqual<string>(Environment.StackTrace, SysInfo.StackTrace);
        }

        [Test]
        [Group(nameof(SysInfo))]
        public void GetDataWidthTest()
        {
        }

        [Test]
        [Group(nameof(SysInfo))]
        public void CurrentDirectoryTest()
        {
            Assert.AreEqual<string>(Environment.CurrentDirectory, SysInfo.CurrentDirectory);
        }

        [Test]
        [Group(nameof(SysInfo))]
        public void DirectoryTest()
        {
            Assert.AreEqual<int>(Environment.CurrentManagedThreadId, SysInfo.CurrentManagedThreadId);
        }

        [Test]
        [Group(nameof(SysInfo))]
        public void GetDrivesTest()
        {
            Assert.EqualElementsOrdered<DriveInfo>(DriveInfo.GetDrives(), SysInfo.DriveInfos);
        }

        [Test]
        [Group(nameof(SysInfo))]
        public void GetDiskDrivesTest()
        {
            SysInfo.GetDiskDrives();
        }

        [Test]
        [RunInThread(ApartmentState.STA, false, "de-DE")]
        [Group(nameof(SysInfo))]
        public void GetUSBDevicesTest()
        {
            List<CimInstance> usbDevices = SysInfo.GetUSBDevices();
            Assert.IsNotNull(usbDevices);
            Assert.IsNotEmpty(usbDevices);
        }

        [Test]
        [RunInThread(ApartmentState.STA, false, "de-DE")]
        [Group(nameof(SysInfo))]
        public void GetUserAccountsTest()
        {
            List<CimInstance> userAccounts = SysInfo.GetUserAccounts();
            Assert.IsNotNull(userAccounts);
            Assert.IsNotEmpty(userAccounts);
        }

        [Test]
        [Group(nameof(SysInfo))]
        public void GetValueTest()
        {
            //SysInfo.GetValue();
        }

        [Group(nameof(SysInfo))]
        [SecurityCritical]
        public void GetWindowsProductKeyTest()
        {
            string key = SysInfo.GetWindowsProductKey();
            Assert.IsTrue(!string.IsNullOrEmpty(key));
        }

        [Group(nameof(SysInfo))]
        [Test]
        public void GetUpTimesTest()
        {
            SysInfo.GetUpTime();
        }

        [Test]
        [Group(nameof(SysInfo))]
        public void GetWinRMInformationTest()
        {
            ServiceControllerStatus serviceControllerStatus = SysInfo.GetWinRMInformation();
            Assert.IsNotNull(serviceControllerStatus);
        }
    }
}
