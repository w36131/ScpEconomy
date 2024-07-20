# ScpEconomy

> [!WARNING]
> Everything in this plugin is subject to change, you can use it at your server but you need to expect data-breaking updates.

## How to install the plugin correctly?

If you want the plugin to have the same data all over yours servers that are hosted on the same machine, put the plugin in /PluginAPI/global, otherwise put it in /PluginAPI/{Your server port}.

As the server loads the plugin, it will create some files in the plugin directory.

![image](https://github.com/user-attachments/assets/f3a5250b-8bc8-434b-8ee4-daecbde8e672)

To validate that everything loaded correctly. Look for any errors in the console from the plugin. If there are none, you are good to go.

## How to add virtual items to the server item shop?

Go to the directory where you installed the plugin, then go to /ScpEconomy/Data, and look for ItemShop.yml, then open it.

![image](https://github.com/user-attachments/assets/533fd320-ada6-4757-94a1-42026eaf35a4)

To add an virtual item paste this into the file:

```
- Name: ExampleVirtualItem
  Description: This is an example virtual item yayyy
  Color:
    r: 255
    g: 255
    b: 255
    a: 255
  Price: 100
  PurchaseActions:
   - !AddToInventory
    Amount: 1
   - !AssignBadge
    BadgeName: owner
    TimeSpan: 01:00:00
   - !ExecuteCommand
    Command: /cassie .g7
```

This will add this item to the server item shop.
Of course you can add more buy just adding the same thing under.

```
- Name: ExampleVirtualItem
  Description: This is an example virtual item yayyy
  Color:
    r: 255
    g: 255
    b: 255
    a: 255
  Price: 100
  PurchaseActions:
   - !AddToInventory
    Amount: 1
   - !AssignBadge
    BadgeName: owner
    TimeSpan: 01:00:00
   - !ExecuteCommand
    Command: /cassie .g7
- Name: ExampleVirtualItem2
  Description: This is a second virtual example item, double yayyy
  Color:
    r: 255
    g: 0
    b: 0
    a: 255
  Price: 200
  PurchaseActions:
   - !AddToInventory
    Amount: 1
   - !AssignBadge
    BadgeName: owner
    TimeSpan: 01:00:00
   - !ExecuteCommand
    Command: /cassie .g7
```

## What are PurchaseActions?

Purchase actions add functionality to your virtual item.
For example:

### AddToInventory
AddToInventory will add the purchased item to the inventory of a player who purchased it. You can specify the amount too.

```
  PurchaseActions:
   - !AddToInventory
    Amount: 1
```

### AssignBadge
AssignBadge will grant the player a specified server role for a specified amount of time, in this example it will give the player the server role "SERVER OWNER" for 1 hour.

```
  PurchaseActions:
   - !AssignBadge
    BadgeName: owner
    TimeSpan: 01:00:00
```

### ExecuteCommand
Execute command will execute a specified command as the server. In this example the server will run a RemoteAdmin command for cassie to say something.

```
  PurchaseActions:
   - !ExecuteCommand
    Command: /cassie .g7
```

