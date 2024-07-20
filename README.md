# ScpEconomy

### How to install the plugin correctly?

If you want the plugin to have the same data all over yours servers that are hosted on the same machine, put the plugin in /PluginAPI/global, otherwise put it in /PluginAPI/{Your server port}.

As the server loads the plugin, it will create some files in the plugin directory.
![image](https://github.com/user-attachments/assets/f3a5250b-8bc8-434b-8ee4-daecbde8e672)
To validate that everything loaded correctly. Look for any errors in the console from the plugin. If there are none, you are good to go.

### How to add items to the server item shop?

Go to the directory where you installed the plugin, then go to /ScpEconomy/Data, and look for ItemShop.yml, then open it.
To add an item paste this into the file:

```
- Name: ExampleItem
  Description: This is an example item yayyy
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
- Name: ExampleItem
  Description: This is an example item yayyy
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
- Name: ExampleItem2
  Description: This is a second example item, double yayyy
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

