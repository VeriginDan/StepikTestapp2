
internal class Car { }
internal class Truck : Car { }
object o1 = new object();
object o2 = new Car();
object o3 = new Truck();
object o4 = o3;
Car car1 = new Car();
Car car2 = new Truck();
Truck truck1 = new Truck();
Car car3 = new Object();
//(1, 12): error CS0266: Не удается неявно преобразовать тип "object" в "Car". Существует явное преобразование (возможно, пропущено приведение типов).
Truck truck2 = new Object();
//(1, 16): error CS0266: Не удается неявно преобразовать тип "object" в "Truck". Существует явное преобразование (возможно, пропущено приведение типов).
Car car4 = truck1;
Truck truck3 = car2;
//(1, 16): error CS0266: Не удается неявно преобразовать тип "Car" в "Truck". Существует явное преобразование (возможно, пропущено приведение типов).
Truck truck4 = (Truck)truck1;
Truck truck5 = (Truck)car2;
Truck truck6 = (Truck)car1;
//System.InvalidCastException: Не удалось привести тип объекта "Car" к типу "Truck".
Car car5 = (Car)o1;
//System.InvalidCastException: Не удалось привести тип объекта "System.Object" к типу "Car".
Car car6 = (Truck)car2;
