# Excerpt from Oracle installation docs for installing on DHCP #
http://download.oracle.com/docs/cd/B28359_01/install.111/b32006/reqs.htm

## 2.6.1 Installing Oracle Database on DHCP Computers ##

Dynamic Host Configuration Protocol (DHCP) assigns dynamic IP addresses on a network. Dynamic addressing allows a computer to have a different IP address each time it connects to the network. In some cases, the IP address can change while the computer is still connected. You can have a mixture of static and dynamic IP addressing in a DHCP system.

In a DHCP setup, the software tracks IP addresses, which simplifies network administration. This lets you add a new computer to the network without having to manually assign that computer a unique IP address. However, before installing Oracle Database onto a computer that uses the DHCP protocol, you need to install a loopback adapter to assign a local IP address to that computer.

See Also:
"Checking if a Loopback Adapter Is Installed on Your Computer"

## 2.6.2 Installing Oracle Database on Computers with Multiple IP Addresses ##

You can install Oracle Database on a computer that has multiple IP addresses, also known as a multihomed computer. Typically, a multihomed computer has multiple network cards. Each IP address is associated with a host name; additionally, you can set up aliases for the host name. By default, Oracle Universal Installer uses the ORACLE\_HOSTNAME environment variable setting to find the host name. If ORACLE\_HOSTNAME is not set and you are installing on a computer that has multiple network cards, Oracle Universal Installer determines the host name by using the first name in the hosts file, typically located in DRIVE\_LETTER:\ WINDOWS\system32\drivers\etc on Windows 2003, Windows Server 2003 [R2](https://code.google.com/p/toolsdotnet/source/detail?r=2), Windows XP, and Windows Vista or DRIVE\_LETTER:\WINNT\system32\drivers\etc on Windows 2000.

Clients must be able to access the computer using this host name, or using aliases for this host name. To check, ping the host name from the client computers using the short name (host name only) and the full name (host name and domain name). Both must work.

Setting the ORACLE\_HOSTNAME Environment Variable

To set the ORACLE\_HOSTNAME environment variable:

Display System in the Windows Control Panel.

In the System Properties dialog box, click Advanced.

In the Advanced tab, click Environment Variables.

In the Environment Variables dialog box, under System Variables, click New.

In the New System Variable dialog box, enter the following information:

Variable name: ORACLE\_HOSTNAME

Variable value: The host name of the computer that you want to use.

Click OK, then in the Environment Variables dialog box, click OK.

Click OK in the Environment Variables dialog box, then in the System Properties dialog box, click OK.
## Installing a loopback adapter ##

http://download.oracle.com/docs/cd/B28359_01/install.111/b32006/reqs.htm#BABGCEAI

2.6.5 Installing a Loopback Adapter

When you install a loopback adapter, the loopback adapter assigns a local IP address for your computer. After the loopback adapter is installed, there are at least two network adapters on your computer: your own network adapter and the loopback adapter. To run Oracle Database on Windows, set the loopback adapter as the primary adapter.

You can change the bind order for the adapters without reinstalling the loopback adapter. The bind order of the adapters to the protocol indicates the order in which the adapters are used. When the loopback adapter is used first for the TCP/IP protocol, all programs that access TCP/IP will first probe the loopback adapter. The local address is used for tools, such as Oracle Enterprise Manager. Any other applications that use a different Ethernet segment will be routed to the network card.

A loopback adapter is required if:

You are installing on a DHCP computer, or

See Also:
"Installing Oracle Database on DHCP Computers"
You are installing on a non-networked computer and plan to connect the computer to a network after installation.

See Also:
"Installing Oracle Database on Non-Networked Computers"
This section covers the following topics:

Checking if a Loopback Adapter Is Installed on Your Computer

Installing a Loopback Adapter on Windows 2000

Installing a Loopback Adapter on Windows 2003, Windows Server 2003 [R2](https://code.google.com/p/toolsdotnet/source/detail?r=2), or Windows XP

Installing a Loopback Adapter on Windows Vista

Removing a Loopback Adapter

2.6.5.1 Checking if a Loopback Adapter Is Installed on Your Computer

To check if a loopback adapter is installed on your computer, run the ipconfig /all command:

DRIVE\_LETTER:\>ipconfig /all
Note:
Loopback Adapter installed on the computer should be made the Primary Network Adapter.
If there is a loopback adapter installed, you would see a section that lists the values for the loopback adapter. For example:

Ethernet adapter Local Area Connection 2:
Connection-specific DNS Suffix  . :
Description . . . . . . . . . . . : Microsoft Loopback Adapter
Physical Address. . . . . . . . . : 02-00-4C-4F-4F-50
DHCP Enabled. . . . . . . . . . . : No
IP Address. . . . . . . . . . . . : 169.254.25.129
Subnet Mask . . . . . . . . . . . : 255.255.0.0
2.6.5.2 Installing a Loopback Adapter on Windows 2000

Windows 2000 reports on the first network adapter installed. This means that if you install additional network adapters after you install the loopback adapter, you need to remove and reinstall the loopback adapter. The loopback adapter must be the last network adapter installed on the computer.

To install a loopback adapter on Windows 2000:

From the Start menu, select Settings, then Control Panel.

Double-click Add/Remove Hardware to start the Add/Remove Hardware wizard.

In the Welcome window, click Next.

In the Choose a Hardware Task window, select Add/Troubleshoot a device, and click Next.

In the Choose a Hardware Device window, select Add a new device, and click Next.

In the Find New Hardware window, select No, I want to select the hardware from a list, and click Next.

In the Hardware Type window, select Network adapters, and click Next.

In the Select Network Adapter window, do the following:

Manufacturers: Select Microsoft.

Network Adapter: Select Microsoft Loopback Adapter.

Click Next.

In the Start Hardware Installation window, click Next.

In the Completing the Add/Remove Hardware Wizard window, click Finish.

Right-click My Network Places on the desktop and select Properties. This displays the Network and Dial-up Connections control panel.

Right-click the connection that was just created. This is usually "Local Area Connection 2". Select Properties.

On the General tab, select Internet Protocol (TCP/IP), and click Properties.

In the Properties dialog box, click Use the following IP address and do the following:

IP Address: Enter a non-routable IP address for the loopback adapter. Oracle recommends the following non-routable addresses:

192.168.x.x (x is any value between 0 and 255)

10.10.10.10

Subnet mask: Enter 255.255.255.0.

Record the values you entered, which you will need later in this procedure.

Leave all other fields empty.

Click OK.

Close the Network Connections window.

Restart the computer.

Add a line to the DRIVE\_LETTER:\WINNT\system32\drivers\etc\hosts file with the following format, right after the localhost line:

IP\_address   hostname.domainname   hostname
where:

IP\_address is the non-routable IP address you entered in step 14.

hostname is the name of the computer.

domainname is the name of the domain.

For example:

10.10.10.10   mycomputer.mydomain.com   mycomputer
Check the network configuration:

Note:
Domain name is optional.
Open System in the Control Panel, and select the Network Identification tab.

In Full computer name, make sure you see the host name and the domain name, for example, sales.us.mycompany.com.

Click Properties.

In Computer name, you should see the host name, and in Full computer name, you should see the host name and domain name. Using the previous example, the host name would be sales and the domain would be us.mycompany.com.

Click More. In Primary DNS suffix of this computer, the domain name, for example, us.mycompany.com, should appear.

Exit the System Control Panel item.

2.6.5.3 Installing a Loopback Adapter on Windows 2003, Windows Server 2003 [R2](https://code.google.com/p/toolsdotnet/source/detail?r=2), or Windows XP

To install a loopback adapter on Windows 2003, Windows Server 2003 [R2](https://code.google.com/p/toolsdotnet/source/detail?r=2), or Windows XP:

Open the Windows Control Panel.

Double-click Add Hardware to start the Add Hardware wizard.

In the Welcome window, click Next.

In the Is the hardware connected? window, select Yes, I have already connected the hardware, and click Next.

In the The following hardware is already installed on your computer window, in the list of installed hardware, select Add a new hardware device, and click Next.

In the The wizard can help you install other hardware window, select Install the hardware that I manually select from a list, and click Next.

From the list of common hardware types, select Network adapters, and click Next.

In the Select Network Adapter window, make the following selections:

Manufacturer: Select Microsoft.

Network Adapter: Select Microsoft Loopback Adapter.

Click Next.

In the The wizard is ready to install your hardware window, click Next.

In the Completing the Add Hardware Wizard window, click Finish.

If you are using Windows 2003, restart your computer.

Right-click My Network Places on the desktop and choose Properties. This displays the Network Connections Control Panel item.

Right-click the connection that was just created. This is usually named "Local Area Connection 2". Choose Properties.

On the General tab, select Internet Protocol (TCP/IP), and click Properties.

In the Properties dialog box, click Use the following IP address and do the following:

IP Address: Enter a non-routable IP for the loopback adapter. Oracle recommends the following non-routable addresses:

192.168.x.x (x is any value between 0 and 255)

10.10.10.10

Subnet mask: Enter 255.255.255.0.

Record the values you entered, which you will need later in this procedure.

Leave all other fields empty.

Click OK.

Click Close.

Close Network Connections.

Restart the computer.

Add a line to the DRIVE\_LETTER:\ WINDOWS\system32\drivers\etc\hosts file with the following format, after the localhost line:

IP\_address   hostname.domainname   hostname
where:

IP\_address is the non-routable IP address you entered in step 16.

hostname is the name of the computer.

domainname is the name of the domain.

For example:

10.10.10.10   mycomputer.mydomain.com   mycomputer
Check the network configuration:

Note:
Domain name is optional.
Open System in the Control Panel, and select the Computer Name tab. In Full computer name, make sure you see the host name and the domain name, for example, sales.us.mycompany.com.

Click Change. In Computer name, you should see the host name, and in Full computer name, you should see the host name and domain name. Using the previous example, the host name would be sales and the domain would be us.mycompany.com.

Click More. In Primary DNS suffix of this computer, you should see the domain name, for example, us.mycompany.com.