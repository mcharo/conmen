conmen
======
Console menu using reflection and custom attributes to automatically build ordered menu.

**Example:**

```
[MenuItem("First menu item")]
public static void firstItem()
{
     Console.WriteLine("First menu item selected");
}
[MenuItem("Second menu item")]
public static void secondItem()
{
     Console.WriteLine("Second menu item selected");
}
```
becomes:  
```
0. First menu item  
1. Second menu item  
```
