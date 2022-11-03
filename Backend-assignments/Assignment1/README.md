# Hero Application
Welcome to Hero application, where you can create different heroes, items and armor!

## Description
This project offers you to create different heroes and equip armour or weapons. You are able to assign different values to these items to strengthen your hero and deal more damage! You can also level up each hero to increase their attributes. The project offers also a way to display all information or get the total amount of attributes the hero has 

## Example
Code below is an example of Creating a Mage object

``` Example Hero
Mage mage = new Mage("hero");
mage.LevelUp();
Weapon weapon = new(WeaponType.Staffs, 12, "Mighty Staff", 1, Slot.Weapon);
mage.Equip(weapon);
mage.Damage();
```

## Licence
Project created by Mohammed Ali Davami
