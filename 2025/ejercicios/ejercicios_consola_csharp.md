# Ejercicios de programación en JavaScript Repartido 1

## Variables

### Ejercicio 1

Declara una variable llamada "nombre" y asígnale tu nombre como valor.

```csharp
// Solucion:
static void GuardarNombre()
{
    string nombre = "Felix";
    Console.WriteLine("La variable 'nombre' vale: " + nombre);
}
```

### Ejercicio 2

Declara una variable llamada "edad" y asígnale tu edad como valor.

```csharp
// Solucion:
int edad = 25;

// Solucion con funcion:
static void GuardarEdad()
{
    int edad = 25;
    Console.WriteLine("La variable 'edad' vale: " + edad);
}
```

### Ejercicio 3

Declara una variable llamada "intereses" y asígnale un array con tus intereses.

```csharp
// Solucion:
string[] hobbies = { "leer", "escuchar música", "ver series" };

// Solucion con funcion:
static void GuardarIntereses()
{
    string[] hobbies = { "leer", "escuchar música", "ver series" };
    Console.WriteLine("Mis hobbies son:");
    foreach (string hobby in hobbies)
    {
        Console.WriteLine("- " + hobby);
    }
}
```

### Ejercicio 4

Declara una variable llamada "esEstudiante" y asígnale un valor booleano que indique si eres estudiante o no.

```csharp
// Solucion:
bool esEstudiante = true;

// Solucion con funcion:
static void GuardarEsEstudiante()
{
    bool esEstudiante = true;
    Console.WriteLine("¿Eres estudiante? " + esEstudiante);
}
```

### Ejercicio 5

Declara una variable llamada "comidaFavorita" y asígnale tu comida favorita como valor.

```csharp
// Solucion:
string comidaFavorita = "pizza";

// Solucion con funcion:
static void GuardarComidaFavorita()
{
    string comidaFavorita = "pizza";
    Console.WriteLine("Mi comida favorita es: " + comidaFavorita);
}
```

### Ejercicio 6

Declara una variable llamada "ciudad" y utiliza `prompt()` para que el usuario ingrese el nombre de su ciudad.  
Luego utiliza `alert()` para mostrar un mensaje que diga "Vives en [ciudad]".

En C# consola:

```csharp
// Solucion:
Console.Write("Ingresa el nombre de tu ciudad: ");
string ciudad = Console.ReadLine();
Console.WriteLine($"Vives en {ciudad}");

// Solucion con funcion:
static void MostrarCiudad(string ciudad)
{
    Console.WriteLine($"Vives en {ciudad}");
}

// Uso:
Console.Write("Ingresa el nombre de tu ciudad: ");
string ciudadUsuario = Console.ReadLine();
MostrarCiudad(ciudadUsuario);
```

### Ejercicio 7

Declara una variable llamada "nombre" y utiliza `prompt()` para que el usuario ingrese su nombre.  
Declara una variable llamada "apellido" y utiliza `prompt()` para que el usuario ingrese su apellido.  
Utiliza `alert()` para mostrar un mensaje que diga "Hola [nombre] [apellido]".

```csharp
// Solucion:
Console.Write("Ingresa tu nombre: ");
string nombre = Console.ReadLine();

Console.Write("Ingresa tu apellido: ");
string apellido = Console.ReadLine();

Console.WriteLine($"Hola {nombre} {apellido}");

// Solucion con funcion:
static void Saludar(string nombre, string apellido)
{
    Console.WriteLine($"Hola {nombre} {apellido}");
}

// Uso:
Console.Write("Ingresa tu nombre: ");
string nombrePersona = Console.ReadLine();

Console.Write("Ingresa tu apellido: ");
string apellidoPersona = Console.ReadLine();

Saludar(nombrePersona, apellidoPersona);
```

### Ejercicio 8

Declara una variable llamada "anoDeNacimiento" y utiliza `prompt()` para que el usuario ingrese su año de nacimiento.  
Calcula su edad y utiliza `alert()` para mostrar un mensaje que diga "Tu edad es [edad]."

```csharp
// Solucion:
Console.Write("Ingresa tu año de nacimiento: ");
int anoDeNacimiento = int.Parse(Console.ReadLine());

int edadCalculada = 2024 - anoDeNacimiento;
Console.WriteLine($"Tu edad es {edadCalculada}");

// Solucion con funcion:
static void CalcularEdad(int anoDeNacimiento)
{
    int edad = 2024 - anoDeNacimiento;
    Console.WriteLine($"Tu edad es {edad}");
}

// Uso:
Console.Write("Ingresa tu año de nacimiento: ");
int anioNac = int.Parse(Console.ReadLine());
CalcularEdad(anioNac);
```

### Ejercicio 9

Declara una variable llamada "cantidadDePersonas" y utiliza `prompt()` para que el usuario ingrese la cantidad de personas con las que va a comer.  
Si asumimos que una persona come una pizza, calcula cuántas pizzas necesitan para comer y muestra un mensaje con la cantidad de pizzas que deben pedir.

```csharp
// Solucion:
Console.Write("Ingresa la cantidad de personas con las que vas a comer: ");
int cantidadDePersonas = int.Parse(Console.ReadLine());
Console.WriteLine($"Deben pedir {cantidadDePersonas} pizzas");

// Solucion con funcion:
static void MostrarCuantasPizzasPedirPorPersona(int cantidadDePersonas)
{
    Console.WriteLine($"Deben pedir {cantidadDePersonas} pizzas");
}

// Uso:
Console.Write("Ingresa la cantidad de personas con las que vas a comer: ");
int personas = int.Parse(Console.ReadLine());
MostrarCuantasPizzasPedirPorPersona(personas);
```

### Ejercicio 10

Declara una variable llamada "pesoDeLasManzanasEnKg" y utiliza `prompt()` para que el usuario ingrese el peso en kilogramos de las manzanas que quiere comprar.  
Declara una variable llamada "precioPorKg" y asígnale un valor de 450.  
Calcula el precio total y muestra un mensaje con el total a pagar.

```csharp
// Solucion:
Console.Write("Ingresa el peso en kilogramos de las manzanas que quieres comprar: ");
float pesoDeLasManzanasEnKg = float.Parse(Console.ReadLine());

float precioPorKg = 450;
float total = pesoDeLasManzanasEnKg * precioPorKg;
Console.WriteLine($"El precio total es ${total}");

// Solucion con funcion:
static float CalcularPrecioManzanas(float pesoDeLasManzanas)
{
    float precioPorKg = 450;
    return pesoDeLasManzanas * precioPorKg;
}

// Uso:
Console.Write("Ingresa el peso en kilogramos de las manzanas que quieres comprar: ");
float pesoIngresado = float.Parse(Console.ReadLine());
float precioTotal = CalcularPrecioManzanas(pesoIngresado);
Console.WriteLine($"El precio total es ${precioTotal}");
```

### Ejercicio 11

La formula para calcular la velocidad en kilómetros por hora es: `velocidad = distancia / tiempo`.  
Ayudemos al usuario a calcular la velocidad promedio de su reciente viaje en omnibus.  
Declara una variable llamada distancia y utiliza `prompt()` para que el usuario ingrese la distancia en kilómetros desde su punto de partida hasta su destino.  
Declara una variable llamada tiempo y utiliza `prompt()` para que el usuario ingrese el tiempo en horas que le tomó llegar a su destino.  
Calcula la velocidad promedio y muestra un mensaje con la velocidad obtenida.

```csharp
// Solucion:
Console.Write("Ingresa la distancia en kilómetros desde tu punto de partida hasta tu destino: ");
int distancia = int.Parse(Console.ReadLine());

Console.Write("Ingresa el tiempo en horas que te tomó llegar a tu destino: ");
int tiempo = int.Parse(Console.ReadLine());

float velocidad = (float)distancia / tiempo;
Console.WriteLine($"La velocidad promedio de tu viaje en ómnibus fue de {velocidad} km/h");

// Solucion con funcion:
static float CalcularVelocidad(int dist, int t)
{
    return (float)dist / t;
}

// Uso:
Console.Write("Ingresa la distancia en kilómetros: ");
int distUsuario = int.Parse(Console.ReadLine());

Console.Write("Ingresa el tiempo en horas: ");
int tiempoUsuario = int.Parse(Console.ReadLine());

float vel = CalcularVelocidad(distUsuario, tiempoUsuario);
Console.WriteLine($"La velocidad promedio de tu viaje en ómnibus fue de {vel} km/h");
```

### Ejercicio 12

La formula para calcular el perímetro de un rectángulo es: `perimetro = 2 * (lado1 + lado2)`.  
Una cancha de futbol 11 mide 110 metros de largo y 75 metros de ancho.  
Si el entrenador manda al equipo a correr 10 kilómetros alrededor de la cancha, cuantas vueltas deberán dar alrededor de la cancha?  
Utiliza un `alert()` para mostrar un mensaje con la cantidad de vueltas que deben dar.

```csharp
// Solucion con modificaciones para uso de funciones
static float CalcularPerimetro(float largo, float ancho)
{
    return 2 * (largo + ancho);
}

static void CalcularCantidadDeVueltas()
{
    float largo = 110;
    float ancho = 75;
    float perimetro = CalcularPerimetro(largo, ancho);
    float vueltas = 10000f / perimetro; // 10 km = 10000 metros

    Console.WriteLine($"Deben dar {vueltas} vueltas alrededor de la cancha");
}

// Uso:
CalcularCantidadDeVueltas();
```

### Ejercicio 13

Declara una variable llamada "numeroDeClientes" y utiliza `prompt()` para que el usuario ingrese la cantidad de clientes que visitaron tu negocio en el día de hoy.  
Declara una variable llamada "ganancias" y utiliza `prompt()` para que el usuario ingrese el monto total de las ganancias del día.  
Calcula el promedio de ganancias por cliente y muestra un mensaje con el promedio obtenido.

```csharp
// Solucion:
Console.Write("Ingresa la cantidad de clientes que visitaron tu negocio hoy: ");
int numeroDeClientes = int.Parse(Console.ReadLine());

Console.Write("Ingresa el monto total de las ganancias del día: ");
float ganancias = float.Parse(Console.ReadLine());

float promedio = ganancias / numeroDeClientes;
Console.WriteLine($"El promedio de ganancias por cliente es de {promedio}");

// Solucion con funcion:
static float CalcularPromedioGanancias(int numeroDeClientes, float ganancias)
{
    return ganancias / numeroDeClientes;
}

// Uso:
Console.Write("Ingresa la cantidad de clientes: ");
int nClientes = int.Parse(Console.ReadLine());

Console.Write("Ingresa el monto total de las ganancias: ");
float gananciaDia = float.Parse(Console.ReadLine());

float prom = CalcularPromedioGanancias(nClientes, gananciaDia);
Console.WriteLine($"El promedio de ganancias por cliente es de {prom}");
```

### Ejercicio 14

Declara una variable llamada "numeroDePersonasInvitadasAComer" y utiliza `prompt()` para que el usuario ingrese la cantidad de personas que invitará a comer.  
Asume que cada persona comerá 2 chivitos. Cada chivito lleva 1 pan tortuga, 1 churrasco, 2 tiras de panceta, 1 huevo frito, 50 gramos de muzzarella y 50 gramos de jamón.  
Calcula cuántos ingredientes necesitarás para preparar la comida y muestra un mensaje con la cantidad de ingredientes necesarios.

```csharp
// Solucion:
Console.Write("Ingresa la cantidad de personas que invitarás a comer: ");
int numeroDePersonasInvitadasAComer = int.Parse(Console.ReadLine());

int chivitosPorPersona = 2;
int panTortugaPorChivito = 1;
int churrascoPorChivito = 1;
int tirasDePancetaPorChivito = 2;
int huevosFritosPorChivito = 1;
int muzzarellaPorChivito = 50;
int jamonPorChivito = 50;

int totalPanTortuga = numeroDePersonasInvitadasAComer * chivitosPorPersona * panTortugaPorChivito;
int totalChurrasco = numeroDePersonasInvitadasAComer * chivitosPorPersona * churrascoPorChivito;
int totalTirasDePanceta = numeroDePersonasInvitadasAComer * chivitosPorPersona * tirasDePancetaPorChivito;
int totalHuevosFritos = numeroDePersonasInvitadasAComer * chivitosPorPersona * huevosFritosPorChivito;
int totalMuzzarella = numeroDePersonasInvitadasAComer * chivitosPorPersona * muzzarellaPorChivito;
int totalJamon = numeroDePersonasInvitadasAComer * chivitosPorPersona * jamonPorChivito;

Console.WriteLine($"Para preparar la comida necesitarás:\n" +
                  $"- {totalPanTortuga} panes tortuga\n" +
                  $"- {totalChurrasco} churrascos\n" +
                  $"- {totalTirasDePanceta} tiras de panceta\n" +
                  $"- {totalHuevosFritos} huevos fritos\n" +
                  $"- {totalMuzzarella} gramos de muzzarella\n" +
                  $"- {totalJamon} gramos de jamón");

// Solucion con funciones:
static (int panTortuga, int churrasco, int tirasPanceta, int huevosFritos, int muzzarella, int jamon) 
    CalcularIngredientes(int cantidadDeChivitos)
{
    int panTortugaPorChivito = 1;
    int churrascoPorChivito = 1;
    int tirasDePancetaPorChivito = 2;
    int huevosFritosPorChivito = 1;
    int muzzarellaPorChivito = 50;
    int jamonPorChivito = 50;

    int totalPanTortuga = cantidadDeChivitos * panTortugaPorChivito;
    int totalChurrasco = cantidadDeChivitos * churrascoPorChivito;
    int totalTirasDePanceta = cantidadDeChivitos * tirasDePancetaPorChivito;
    int totalHuevosFritos = cantidadDeChivitos * huevosFritosPorChivito;
    int totalMuzzarella = cantidadDeChivitos * muzzarellaPorChivito;
    int totalJamon = cantidadDeChivitos * jamonPorChivito;

    return (totalPanTortuga, totalChurrasco, totalTirasDePanceta, 
            totalHuevosFritos, totalMuzzarella, totalJamon);
}

static int CalcularCantidadDeChivitosTotales(int cantidadDePersonasInvitadasAComer, int cantidadDeChivitosPorPersona)
{
    return cantidadDePersonasInvitadasAComer * cantidadDeChivitosPorPersona;
}

// Uso:
Console.Write("Ingresa la cantidad de personas que invitarás a comer: ");
int cantidadDePersonas = int.Parse(Console.ReadLine());

Console.Write("Ingresa la cantidad de chivitos que comerá cada persona: ");
int chivitosPorPersonaUsuario = int.Parse(Console.ReadLine());

// Sumar 1 a la cantidad de chivitos por persona (por lo visto en el ejemplo)
chivitosPorPersonaUsuario += 1;

int chivitosTotales = CalcularCantidadDeChivitosTotales(cantidadDePersonas, chivitosPorPersonaUsuario);
var ingredientes = CalcularIngredientes(chivitosTotales);

Console.WriteLine("Para preparar la comida necesitarás:" +
    $"\n- {ingredientes.panTortuga} panes tortuga" +
    $"\n- {ingredientes.churrasco} churrascos" +
    $"\n- {ingredientes.tirasPanceta} tiras de panceta" +
    $"\n- {ingredientes.huevosFritos} huevos fritos" +
    $"\n- {ingredientes.muzzarella} gramos de muzzarella" +
    $"\n- {ingredientes.jamon} gramos de jamón");
```

### Ejercicio 15

Declara una variable llamada "manzanasCompradas" y utiliza `prompt()` para que el usuario ingrese la cantidad de manzanas que compró.  
Declara una variable llamada "manzanasComidasPorLosHijos" y utiliza `prompt()` para que el usuario ingrese la cantidad de manzanas que se comieron los hijos.  
Declara una variable llamada "manzanasComidasPorLosPadres" y utiliza `prompt()` para que el usuario ingrese la cantidad de manzanas que se comieron los padres.  
Calcula cuantas manzanas quedan y muestra un mensaje con la cantidad de manzanas restantes.

```csharp
// Solucion:
Console.Write("Ingresa la cantidad de manzanas que compraste: ");
int manzanasCompradas = int.Parse(Console.ReadLine());

Console.Write("Ingresa la cantidad de manzanas que se comieron los hijos: ");
int manzanasComidasPorLosHijos = int.Parse(Console.ReadLine());

Console.Write("Ingresa la cantidad de manzanas que se comieron los padres: ");
int manzanasComidasPorLosPadres = int.Parse(Console.ReadLine());

int manzanasRestantes = manzanasCompradas - manzanasComidasPorLosHijos - manzanasComidasPorLosPadres;
Console.WriteLine($"Quedan {manzanasRestantes} manzanas");
```

### Ejercicio 16

La fórmula para calcular el área de un rectángulo es: `area = largo * ancho`.  
El usuario es un agente inmobiliario y recién llega de medir una casa.  
La casa tiene 3 cuartos, un baño, una cocina y un living-comedor.  
Pedir al usuario que ingrese largo y ancho de cada espacio.  
Mostrar un mensaje con el área total de la casa.  
Mostrar un mensaje con el área total de todos los cuartos.  
Si el precio por metro cuadrado es de U$S 2600, mostrar un mensaje con el precio total de la casa.

```csharp
// Solucion:
Console.Write("Ingresa el largo del cuarto 1: ");
int largoCuarto1 = int.Parse(Console.ReadLine());
Console.Write("Ingresa el ancho del cuarto 1: ");
int anchoCuarto1 = int.Parse(Console.ReadLine());

Console.Write("Ingresa el largo del cuarto 2: ");
int largoCuarto2 = int.Parse(Console.ReadLine());
Console.Write("Ingresa el ancho del cuarto 2: ");
int anchoCuarto2 = int.Parse(Console.ReadLine());

Console.Write("Ingresa el largo del cuarto 3: ");
int largoCuarto3 = int.Parse(Console.ReadLine());
Console.Write("Ingresa el ancho del cuarto 3: ");
int anchoCuarto3 = int.Parse(Console.ReadLine());

Console.Write("Ingresa el largo del baño: ");
int largoBano = int.Parse(Console.ReadLine());
Console.Write("Ingresa el ancho del baño: ");
int anchoBano = int.Parse(Console.ReadLine());

Console.Write("Ingresa el largo de la cocina: ");
int largoCocina = int.Parse(Console.ReadLine());
Console.Write("Ingresa el ancho de la cocina: ");
int anchoCocina = int.Parse(Console.ReadLine());

Console.Write("Ingresa el largo del living-comedor: ");
int largoLivingComedor = int.Parse(Console.ReadLine());
Console.Write("Ingresa el ancho del living-comedor: ");
int anchoLivingComedor = int.Parse(Console.ReadLine());

int areaCuarto1 = largoCuarto1 * anchoCuarto1;
int areaCuarto2 = largoCuarto2 * anchoCuarto2;
int areaCuarto3 = largoCuarto3 * anchoCuarto3;
int areaBano = largoBano * anchoBano;
int areaCocina = largoCocina * anchoCocina;
int areaLivingComedor = largoLivingComedor * anchoLivingComedor;

int areaTotal = areaCuarto1 + areaCuarto2 + areaCuarto3 + areaBano + areaCocina + areaLivingComedor;
int areaTotalCuartos = areaCuarto1 + areaCuarto2 + areaCuarto3;

int precioPorMetroCuadrado = 2600;
int precioTotal = areaTotal * precioPorMetroCuadrado;

Console.WriteLine($"El área total de la casa es de {areaTotal} metros cuadrados.");
Console.WriteLine($"El área total de los cuartos es de {areaTotalCuartos} metros cuadrados.");
Console.WriteLine($"El precio total de la casa es de U$S {precioTotal}");

// Solucion con funciones:
static int PedirLargoAnchoYCalcularArea(string habitacion)
{
    Console.Write($"Ingresa el largo de {habitacion}: ");
    int largo = int.Parse(Console.ReadLine());

    Console.Write($"Ingresa el ancho de {habitacion}: ");
    int ancho = int.Parse(Console.ReadLine());

    // En el ejemplo, se sumaba 1 a cada lado; replicamos la idea
    return (largo + 1) * (ancho + 1);
}

// Uso:
int areaC1 = PedirLargoAnchoYCalcularArea("el cuarto 1");
int areaC2 = PedirLargoAnchoYCalcularArea("el cuarto 2");
int areaC3 = PedirLargoAnchoYCalcularArea("el cuarto 3");
int areaB = PedirLargoAnchoYCalcularArea("el baño");
int areaCo = PedirLargoAnchoYCalcularArea("la cocina");
int areaL = PedirLargoAnchoYCalcularArea("el living-comedor");

int totalAreas = areaC1 + areaC2 + areaC3 + areaB + areaCo + areaL;
int totalCuartos = areaC1 + areaC2 + areaC3;
int precioFinal = totalAreas * 2600;

Console.WriteLine($"El área total de la casa es de {totalAreas} metros cuadrados.");
Console.WriteLine($"El área total de los cuartos es de {totalCuartos} metros cuadrados.");
Console.WriteLine($"El precio total de la casa es de U$S {precioFinal}");
```

### Ejercicio 17

La formula para calcular la velocidad de un vehículo a partir de las revoluciones del motor es:  
```
velocidad = ((revolucionesEnRPM * diametroDeLaRuedaEnMetros * 3.1416) / (ratioDeMarcha * diferencialFinal)) * (60 / 1000)
```
Ejemplo con valores fijos (a excepción de RPM o diámetro).

#### Parte 1:
Asumiendo que utilizamos los valores del ejemplo, a excepción de las revoluciones del motor, pedir al usuario que ingrese las revoluciones del motor.  
Mostrar un mensaje con la velocidad obtenida.

#### Parte 2:
Asumiendo que utilizamos los valores del ejemplo, a excepción del diámetro de la rueda y las revoluciones del motor.  
Pedir al usuario que ingrese el diámetro de la rueda en metros y las revoluciones del motor.  
Mostrar un mensaje con la velocidad obtenida.

```csharp
// Solucion Parte 1:
Console.Write("Ingresa las revoluciones del motor (RPM): ");
int revolucionesEnRPM = int.Parse(Console.ReadLine());

double diametroDeLaRuedaEnMetros = 0.6;
double ratioDeMarcha = 0.8;
double diferencialFinal = 4.1;

double velocidad = ((revolucionesEnRPM * diametroDeLaRuedaEnMetros * 3.1416) 
                   / (ratioDeMarcha * diferencialFinal)) * (60.0 / 1000.0);

Console.WriteLine($"La velocidad del vehículo es de {velocidad} km/h");

// Solucion Parte 2:
Console.Write("Ingresa el diámetro de la rueda en metros: ");
diametroDeLaRuedaEnMetros = double.Parse(Console.ReadLine());

Console.Write("Ingresa las revoluciones del motor (RPM): ");
revolucionesEnRPM = int.Parse(Console.ReadLine());

velocidad = ((revolucionesEnRPM * diametroDeLaRuedaEnMetros * 3.1416) 
            / (ratioDeMarcha * diferencialFinal)) * (60.0 / 1000.0);

Console.WriteLine($"La velocidad del vehículo es de {velocidad} km/h");
```

## Condicionales y operadores lógicos

### Ejercicio 18

Declara una variable llamada "edad" y utiliza `prompt()` para que el usuario ingrese su edad.  
Si la edad es menor a 18, muestra un mensaje con "Eres menor de edad".  
Si la edad es mayor o igual a 18, muestra un mensaje con "Eres mayor de edad".

```csharp
// Solucion:
Console.Write("Ingresa tu edad: ");
int edad = int.Parse(Console.ReadLine());

if (edad < 18)
{
    Console.WriteLine("Eres menor de edad");
}
else
{
    Console.WriteLine("Eres mayor de edad");
}

// Solucion con funcion:
static void MostrarMensajeDeEdad(int edad)
{
    if (edad < 18)
    {
        Console.WriteLine("Eres menor de edad");
    }
    else
    {
        Console.WriteLine("Eres mayor de edad");
    }
}
```

### Ejercicio 19

Declara una variable llamada "numero" y utiliza `prompt()` para que el usuario ingrese un número.  
Si el número es positivo, muestra un mensaje con "El número es positivo".  
Si el número es negativo, muestra un mensaje con "El número es negativo".

```csharp
// Solucion:
Console.Write("Ingresa un número: ");
int numero = int.Parse(Console.ReadLine());

if (numero > 0)
{
    Console.WriteLine("El número es positivo");
}
else
{
    Console.WriteLine("El número es negativo");
}
```

### Ejercicio 20

Pide al usuario que ingrese su edad.  
Calcula el año de nacimiento y muestra un mensaje con el año de nacimiento.  
Si la edad es menor a 0, muestra un mensaje de error.  
Si la edad es mayor a 100, muestra un mensaje de error.

```csharp
// Solucion:
Console.Write("Ingresa tu edad: ");
int edadUsuario = int.Parse(Console.ReadLine());

int anoDeNacimiento = 2024 - edadUsuario;

if (edadUsuario < 0)
{
    Console.WriteLine("Error: La edad no puede ser menor a 0");
}
else if (edadUsuario > 100)
{
    Console.WriteLine("Error: La edad no puede ser mayor a 100");
}
else
{
    Console.WriteLine($"Tu año de nacimiento es {anoDeNacimiento}");
}

// Solucion con funcion:
static int CalcularAnoDeNacimiento(int edad)
{
    return 2024 - edad;
}

static void MostrarAnoNacimientoValidado(int edad)
{
    int anio = CalcularAnoDeNacimiento(edad);
    if (edad < 0)
    {
        Console.WriteLine("Error: La edad no puede ser menor a 0");
    }
    else if (edad > 100)
    {
        Console.WriteLine("Error: La edad no puede ser mayor a 100");
    }
    else
    {
        Console.WriteLine($"Tu año de nacimiento es {anio}");
    }
}
```

### Ejercicio 21

Pide al usuario que ingrese si tiene bicicleta.  
Si el usuario tiene bicicleta, muestra un mensaje con "Tienes bicicleta, que nivel".  
Si el usuario no tiene bicicleta, muestra un mensaje con "No tienes bicicleta, que nivel".

```csharp
// Solucion:
Console.Write("¿Tienes bicicleta? (si/no): ");
string tieneBicicleta = Console.ReadLine().ToLower();

if (tieneBicicleta == "si")
{
    Console.WriteLine("Tienes bicicleta, que nivel");
}
else
{
    Console.WriteLine("No tienes bicicleta, que nivel");
}

// Solucion con funcion:
static bool TransformarRespuestaABoolean(string respuesta)
{
    return respuesta.ToLower() == "si";
}

static void MostrarMensajeDeBicicleta(string respuesta)
{
    bool respondioQueSi = TransformarRespuestaABoolean(respuesta);

    if (respondioQueSi)
    {
        Console.WriteLine("Tienes bicicleta, que nivel");
    }
    else
    {
        Console.WriteLine("No tienes bicicleta, que nivel");
    }
}

// Uso:
Console.Write("¿Tienes bicicleta? (si/no): ");
string resp = Console.ReadLine();
MostrarMensajeDeBicicleta(resp);
```

### Ejercicio 22

Pide al usuario que ingrese cuantos pares de zapatos tiene.  
- Si el usuario tiene menos de 2 pares de zapatos, muestra el mensaje "Calidad por sobre cantidad!".  
- Si el usuario tiene entre 2 y 5 pares de zapatos, muestra el mensaje "Lo justo para cada ocación!".  
- Si el usuario tiene más de 5 pares de zapatos, muestra el mensaje "Una elección dificil todos los dias!".

```csharp
// Solucion:
Console.Write("¿Cuántos pares de zapatos tienes?: ");
int paresDeZapatos = int.Parse(Console.ReadLine());

if (paresDeZapatos < 2)
{
    Console.WriteLine("Calidad por sobre cantidad!");
}
else if (paresDeZapatos >= 2 && paresDeZapatos <= 5)
{
    Console.WriteLine("Lo justo para cada ocación!");
}
else
{
    Console.WriteLine("Una elección dificil todos los dias!");
}
```

### Ejercicio 23

El usuario está a punto de cambiar su contraseña.  
Pide al usuario que ingrese su nueva contraseña.  
Pide al usuario que ingrese su nueva contraseña nuevamente.  
Si las contraseñas son iguales, muestra un mensaje con "Contraseña cambiada con exito".  
Si las contraseñas son diferentes, muestra un mensaje con "Las contraseñas no coinciden".

```csharp
// Solucion:
Console.Write("Ingresa tu nueva contraseña: ");
string contrasena1 = Console.ReadLine();

Console.Write("Ingresa tu nueva contraseña nuevamente: ");
string contrasena2 = Console.ReadLine();

if (contrasena1 == contrasena2)
{
    Console.WriteLine("Contraseña cambiada con exito");
}
else
{
    Console.WriteLine("Las contraseñas no coinciden");
}
```

### Ejercicio 24

Pide al usuario que ingrese 3 numeros.  
- Si el primer número es menor que el segundo, y el segundo menor que el tercero, muestra un mensaje con "Los números están en orden creciente. Que ordenado!".  
- Si el primer número es mayor que el segundo, y el segundo mayor que el tercero, muestra un mensaje con "Los números están en orden decreciente. Que ordenado!".  
- Si no se cumple ninguna de las condiciones anteriores, muestra un mensaje con "Los números no están ordenados. Que caotico".

```csharp
// Solucion:
Console.Write("Ingresa el primer número: ");
int numero1 = int.Parse(Console.ReadLine());

Console.Write("Ingresa el segundo número: ");
int numero2 = int.Parse(Console.ReadLine());

Console.Write("Ingresa el tercer número: ");
int numero3 = int.Parse(Console.ReadLine());

if (numero1 < numero2 && numero2 < numero3)
{
    Console.WriteLine("Los números están en orden creciente. Que ordenado!");
}
else if (numero1 > numero2 && numero2 > numero3)
{
    Console.WriteLine("Los números están en orden decreciente. Que ordenado!");
}
else
{
    Console.WriteLine("Los números no están ordenados. Que caotico");
}
```

### Ejercicio 25

Pide al usuario que ingrese cuantas zanahorias ha comido en lo que va del año.  
- Si el número es mayor a 0, muestra un mensaje con "Que saludable! Llevas la cuenta?".  
- Si el usuario ingresa un texto vacío, o letras, muestra un mensaje con "No te preocupes, sería raro que lleves la cuenta".

```csharp
// Solucion:
Console.Write("Cuantas zanahorias has comido en lo que va del año?: ");
string inputZanahorias = Console.ReadLine();

int cantidad;
bool esNumero = int.TryParse(inputZanahorias, out cantidad);

if (esNumero && cantidad > 0)
{
    Console.WriteLine("Que saludable! Llevas la cuenta?");
}
else
{
    Console.WriteLine("No te preocupes, sería raro que lleves la cuenta");
}
```

### Ejercicio 27

Pide al usuario que ingrese un número, luego una letra cualquiera, luego una vocal, luego una consonante, y luego nuevamente el número inicial.  
- Si el usuario ingresó el mismo número al principio y al final, muestra "Bien hecho!".  
- Si no es el mismo número, muestra "Error!".

```csharp
// Solucion:
Console.Write("Ingresa un número: ");
int numeroInicial = int.Parse(Console.ReadLine());

Console.Write("Ingresa una letra cualquiera: ");
string letra = Console.ReadLine();

Console.Write("Ingresa una vocal: ");
string vocal = Console.ReadLine();

Console.Write("Ingresa una consonante: ");
string consonante = Console.ReadLine();

Console.Write("Ingresa nuevamente el número que ingresaste al principio: ");
int numeroFinal = int.Parse(Console.ReadLine());

if (numeroInicial == numeroFinal)
{
    Console.WriteLine("Bien hecho!");
}
else
{
    Console.WriteLine("Error!");
}
```

### Ejercicio 28

Pide al usuario que ingrese un número, un segundo número, y un tercer número.  
- Si el primer numero es igual al tercero, y el segundo es diferente al primero, muestra "Los números 1, y 3, son iguales y el segundo es diferente al primero".  
- Si el primer número es igual al segundo, y el segundo es diferente al tercero, muestra "Los números 1, y 2, son iguales y el segundo es diferente al tercero".  
- Si todos los números son iguales, muestra "Todos los números son iguales".

```csharp
// Solucion:
Console.Write("Ingresa el primer número: ");
int numero1_28 = int.Parse(Console.ReadLine());

Console.Write("Ingresa el segundo número: ");
int numero2_28 = int.Parse(Console.ReadLine());

Console.Write("Ingresa el tercer número: ");
int numero3_28 = int.Parse(Console.ReadLine());

if (numero1_28 == numero3_28 && numero2_28 != numero1_28)
{
    Console.WriteLine("Los números 1, y 3, son iguales y el segundo es diferente al primero");
}
else if (numero1_28 == numero2_28 && numero2_28 != numero3_28)
{
    Console.WriteLine("Los números 1, y 2, son iguales y el segundo es diferente al tercero");
}
else if (numero1_28 == numero2_28 && numero2_28 == numero3_28)
{
    Console.WriteLine("Todos los números son iguales");
}
```

### Ejercicio 29

Pide al usuario que ingrese un múltiplo de 37.  
- Si el usuario ingresa un número que no es múltiplo de 37, muestra "Error! Ese número no es múltiplo de 37".  
- Si el usuario ingresa un número que sí es múltiplo de 37, muestra "Bien hecho!".

```csharp
// Solucion:
Console.Write("Ingresa un múltiplo de 37: ");
int numero29 = int.Parse(Console.ReadLine());

if (numero29 % 37 == 0)
{
    Console.WriteLine("Bien hecho!");
}
else
{
    Console.WriteLine("Error! Ese número no es múltiplo de 37");
}
```

### Ejercicio 30

Pide al usuario que ingrese el nombre de una constelación.  
- Si ingresa "orión", "cinturon de orion", "3 marias", o "tres marias", muestra "Esa constelación es muy conocida".  
- Si ingresa "cruz del sur" o "Cruz Del Sur", muestra "Asi que te encuentras en el hemisferio sur!".  
- Si ingresa "osa mayor", "Osa Mayor", "osa menor", o "Osa Menor", muestra "Asi que te encuentras en el hemisferio norte!".  
- En cualquier otro caso, muestra "Muy bien... creo".

```csharp
// Solucion:
Console.Write("Ingresa el nombre de una constelación: ");
string constelacion = Console.ReadLine();

if (constelacion == "orión" || constelacion == "cinturon de orion" ||
    constelacion == "3 marias" || constelacion == "tres marias")
{
    Console.WriteLine("Esa constelación es muy conocida");
}
else if (constelacion == "cruz del sur" || constelacion == "Cruz Del Sur")
{
    Console.WriteLine("Asi que te encuentras en el hemisferio sur!");
}
else if (constelacion == "osa mayor" || constelacion == "Osa Mayor" ||
         constelacion == "osa menor" || constelacion == "Osa Menor")
{
    Console.WriteLine("Asi que te encuentras en el hemisferio norte!");
}
else
{
    Console.WriteLine("Muy bien... creo");
}
```

### Ejercicio 31

Pide al usuario que ingrese el número del mes de su nacimiento, y el número del día de su nacimiento.  
Muestra un mensaje al usuario con su signo zodiacal según la tabla dada (Aries, Tauro, etc.).

```csharp
// Solucion:
Console.Write("Ingresa el número del mes de tu nacimiento (1-12): ");
int mes = int.Parse(Console.ReadLine());

Console.Write("Ingresa el número del día de tu nacimiento (1-31): ");
int dia = int.Parse(Console.ReadLine());

// Usamos la misma lógica condicional
if ((mes == 3 && dia >= 21) || (mes == 4 && dia <= 19))
{
    Console.WriteLine("Tu signo zodiacal es Aries");
}
else if ((mes == 4 && dia >= 20) || (mes == 5 && dia <= 20))
{
    Console.WriteLine("Tu signo zodiacal es Tauro");
}
else if ((mes == 5 && dia >= 21) || (mes == 6 && dia <= 20))
{
    Console.WriteLine("Tu signo zodiacal es Géminis");
}
else if ((mes == 6 && dia >= 21) || (mes == 7 && dia <= 22))
{
    Console.WriteLine("Tu signo zodiacal es Cáncer");
}
else if ((mes == 7 && dia >= 23) || (mes == 8 && dia <= 22))
{
    Console.WriteLine("Tu signo zodiacal es Leo");
}
else if ((mes == 8 && dia >= 23) || (mes == 9 && dia <= 22))
{
    Console.WriteLine("Tu signo zodiacal es Virgo");
}
else if ((mes == 9 && dia >= 23) || (mes == 10 && dia <= 22))
{
    Console.WriteLine("Tu signo zodiacal es Libra");
}
else if ((mes == 10 && dia >= 23) || (mes == 11 && dia <= 21))
{
    Console.WriteLine("Tu signo zodiacal es Escorpio");
}
else if ((mes == 11 && dia >= 22) || (mes == 12 && dia <= 21))
{
    Console.WriteLine("Tu signo zodiacal es Sagitario");
}
else if ((mes == 12 && dia >= 22) || (mes == 1 && dia <= 19))
{
    Console.WriteLine("Tu signo zodiacal es Capricornio");
}
else if ((mes == 1 && dia >= 20) || (mes == 2 && dia <= 18))
{
    Console.WriteLine("Tu signo zodiacal es Acuario");
}
else if ((mes == 2 && dia >= 19) || (mes == 3 && dia <= 20))
{
    Console.WriteLine("Tu signo zodiacal es Piscis");
}
```

### Ejercicio 33

Alguien estuvo jugando al ta-te-ti y el tablero quedó así:

```
  X  |  -  |  O
  -  |  -  |  -
  X  |  -  |  -
```

Filas: 1 a 3 (arriba a abajo), Columnas: 1 a 3 (izq a der).  
- Si el usuario ingresa fila 2 y columna 1, muestra "Genio de la estrategia mundial! bloqueaste la victoria de X!".  
- Si el usuario ingresa valores que corresponden a una casilla ocupada, muestra "Esa casilla ya está ocupada!".  
- Si el usuario ingresa valores que no corresponden a una casilla, muestra "Esa casilla no existe!".  
- Si el usuario ingresa otra casilla que no bloquee la victoria de X, muestra "Mmm... X gana en el siguiente turno!".

```csharp
// Solucion:
Console.WriteLine("Dado el siguiente tablero de ta-te-ti:\n  X  |  -  |  O\n  -  |  -  |  -\n  X  |  -  |  -\nTe toca jugar con 'O'!");

Console.Write("Ingresa el número de la fila (1-3): ");
int fila = int.Parse(Console.ReadLine());

Console.Write("Ingresa el número de la columna (1-3): ");
int columna = int.Parse(Console.ReadLine());

if (fila == 2 && columna == 1)
{
    Console.WriteLine("Genio de la estrategia mundial! bloqueaste la victoria de X!");
}
else if ((fila == 1 && columna == 1) || 
         (fila == 1 && columna == 3) ||
         (fila == 3 && columna == 1))
{
    Console.WriteLine("Esa casilla ya está ocupada!");
}
else if (fila < 1 || fila > 3 || columna < 1 || columna > 3)
{
    Console.WriteLine("Esa casilla no existe!");
}
else
{
    Console.WriteLine("Mmm... X gana en el siguiente turno!");
}
```

### Ejercicio 34

Pide al usuario que ingrese tres marcas de auto.  
- Si el usuario ingresa "ferrari", "lamborghini", o "porsche" en cualquiera de sus respuestas, muestra "Asi que te gustan los autos deportivos!".  
- Si ingresa "toyota", "honda", o "nissan" en cualquiera de sus respuestas, muestra "Asi que te gustan los autos japoneses!".  
- Si ingresa "ford", "chevrolet", o "dodge" en cualquiera de sus respuestas, muestra "Asi que te gustan los autos americanos!".

```csharp
// Solucion:
Console.Write("Ingresa una marca de auto: ");
string marca1 = Console.ReadLine().ToLower();

Console.Write("Ingresa otra marca de auto: ");
string marca2 = Console.ReadLine().ToLower();

Console.Write("Ingresa una tercera marca de auto: ");
string marca3 = Console.ReadLine().ToLower();

bool deportivos = (marca1 == "ferrari" || marca2 == "ferrari" || marca3 == "ferrari") ||
                  (marca1 == "lamborghini" || marca2 == "lamborghini" || marca3 == "lamborghini") ||
                  (marca1 == "porsche" || marca2 == "porsche" || marca3 == "porsche");

bool japoneses = (marca1 == "toyota" || marca2 == "toyota" || marca3 == "toyota") ||
                 (marca1 == "honda" || marca2 == "honda" || marca3 == "honda") ||
                 (marca1 == "nissan" || marca2 == "nissan" || marca3 == "nissan");

bool americanos = (marca1 == "ford" || marca2 == "ford" || marca3 == "ford") ||
                  (marca1 == "chevrolet" || marca2 == "chevrolet" || marca3 == "chevrolet") ||
                  (marca1 == "dodge" || marca2 == "dodge" || marca3 == "dodge");

if (deportivos)
{
    Console.WriteLine("Asi que te gustan los autos deportivos!");
}
else if (japoneses)
{
    Console.WriteLine("Asi que te gustan los autos japoneses!");
}
else if (americanos)
{
    Console.WriteLine("Asi que te gustan los autos americanos!");
}
```

## Bucles o Ciclos

### Ejercicio 35

Muestra los números del 1 al 10 utilizando un bucle `for` y `alert()`.  
(En C#, usaremos `Console.WriteLine`).

```csharp
// Solucion:
for (int i = 1; i <= 10; i++)
{
    Console.WriteLine(i);
}
```

### Ejercicio 36

Suma los números del 1 al 10 utilizando un bucle `for` y muestra el resultado.

```csharp
// Solucion:
int suma = 0;
for (int i = 1; i <= 10; i++)
{
    suma += i;
}
Console.WriteLine($"La suma de 1 a 10 es: {suma}");
```

### Ejercicio 37

Muestra los números pares del 1 al 10 utilizando un bucle `for`.

```csharp
// Solucion:
for (int i = 2; i <= 10; i += 2)
{
    Console.WriteLine(i);
}
```

### Ejercicio 38

Pide al usuario un número.  
Muestra los números impares del 1 al número ingresado utilizando un bucle `for`.

```csharp
// Solucion:
Console.Write("Ingresa un número: ");
int numero38 = int.Parse(Console.ReadLine());

for (int i = 1; i <= numero38; i += 2)
{
    Console.WriteLine(i);
}
```

### Ejercicio 39

Crea un Array vacío con el nombre "nombres".  
Pide al usuario que ingrese 3 nombres y guárdalos en el array.  
Muestra los nombres ingresados utilizando un bucle `for`.

```csharp
// Solucion:
List<string> nombres = new List<string>();

Console.Write("Ingresa un nombre: ");
nombres.Add(Console.ReadLine());

Console.Write("Ingresa otro nombre: ");
nombres.Add(Console.ReadLine());

Console.Write("Ingresa un tercer nombre: ");
nombres.Add(Console.ReadLine());

for (int i = 0; i < nombres.Count; i++)
{
    Console.WriteLine(nombres[i]);
}
```

### Ejercicio 40

Crea un Array vacío con el nombre "numeros".  
Pide al usuario que ingrese 3 números y guárdalos en el array.  
Muestra los números ingresados utilizando un bucle `for`.

```csharp
// Solucion:
List<int> numeros = new List<int>();

Console.Write("Ingresa un número: ");
numeros.Add(int.Parse(Console.ReadLine()));

Console.Write("Ingresa otro número: ");
numeros.Add(int.Parse(Console.ReadLine()));

Console.Write("Ingresa un tercer número: ");
numeros.Add(int.Parse(Console.ReadLine()));

for (int i = 0; i < numeros.Count; i++)
{
    Console.WriteLine(numeros[i]);
}
```

### Ejercicio 41

Crea un Array vacío con el nombre "sumandos".  
Pide al usuario que ingrese 3 números y guárdalos en el array.  
Muestra el total de la suma de todos los números ingresados.

```csharp
// Solucion:
List<int> sumandos = new List<int>();

Console.Write("Ingresa un número: ");
sumandos.Add(int.Parse(Console.ReadLine()));

Console.Write("Ingresa otro número: ");
sumandos.Add(int.Parse(Console.ReadLine()));

Console.Write("Ingresa un tercer número: ");
sumandos.Add(int.Parse(Console.ReadLine()));

int suma41 = 0;
for (int i = 0; i < sumandos.Count; i++)
{
    suma41 += sumandos[i];
}
Console.WriteLine($"La suma total es: {suma41}");
```

### Ejercicio 42

Pide al usuario un número y guárdalo en "cantidadDeNombres".  
Crea un Array vacío con el nombre "nombres".  
Pide al usuario que ingrese tantos nombres como la cantidad dada.  
Muestra los nombres ingresados utilizando un bucle `for`.

```csharp
// Solucion:
Console.Write("Ingresa la cantidad de nombres que deseas ingresar: ");
int cantidadDeNombres = int.Parse(Console.ReadLine());

List<string> listaNombres = new List<string>();

for (int i = 0; i < cantidadDeNombres; i++)
{
    Console.Write("Ingresa un nombre: ");
    listaNombres.Add(Console.ReadLine());
}

for (int i = 0; i < listaNombres.Count; i++)
{
    Console.WriteLine(listaNombres[i]);
}
```

### Ejercicio 43

Crea un bucle `while`.  
- Dentro del bucle, pide al usuario que ingrese un número.  
- Muestra un mensaje con "El número ingresado es: X".  
- Muestra un mensaje con "Si escribes 'salir' saldrás del bucle".  
- Como condición de salida del bucle, evalúa si el usuario ingresó "salir".

```csharp
// Solucion:
string numero43 = "";

while (numero43 != "salir")
{
    Console.Write("Ingresa un número: ");
    numero43 = Console.ReadLine();
    Console.WriteLine($"El número ingresado es: {numero43}");
    Console.WriteLine("Si escribes 'salir' saldrás del bucle");
}
```

### Ejercicio 44

Utilizando un bucle `while`, pide al usuario que ingrese tantos nombres como quiera.  
Guarda cada nombre en un array.  
Termina el bucle cuando el usuario ingrese la palabra "salir".  
Muestra los nombres ingresados utilizando un bucle `for` y `alert()` (en C#, `Console.WriteLine`).

```csharp
// Solucion:
List<string> nombres44 = new List<string>();
string inputNombre = "";

while (inputNombre != "salir")
{
    Console.Write("Ingresa un nombre (o 'salir' para terminar): ");
    inputNombre = Console.ReadLine();
    if (inputNombre != "salir")
    {
        nombres44.Add(inputNombre);
    }
}

for (int i = 0; i < nombres44.Count; i++)
{
    Console.WriteLine(nombres44[i]);
}
```

### Ejercicio 45

Utilizando un bucle `while`, pide al usuario que ingrese tantos números como quiera.  
Guarda cada número en un array.  
Termina el bucle cuando el usuario ingrese la palabra "salir".  
Muestra el promedio de los números ingresados.

```csharp
// Solucion:
List<int> numeros45 = new List<int>();
string entrada = "";

while (entrada != "salir")
{
    Console.Write("Ingresa un número (o 'salir' para terminar): ");
    entrada = Console.ReadLine();

    if (entrada != "salir")
    {
        if (int.TryParse(entrada, out int numeroValido))
        {
            numeros45.Add(numeroValido);
        }
    }
}

if (numeros45.Count > 0)
{
    int suma45 = 0;
    foreach (int num in numeros45)
    {
        suma45 += num;
    }
    double promedio45 = (double)suma45 / numeros45.Count;
    Console.WriteLine($"El promedio de los números ingresados es: {promedio45}");
}
else
{
    Console.WriteLine("No se ingresaron números.");
}
```

### Ejercicio 46

Utilizando un bucle `while`, pide al usuario que ingrese tantos alumnos como quiera.  
Para cada alumno pregunta nombre y apellido.  
Guarda cada alumno en un array.  
Termina el bucle cuando el usuario ingrese la palabra "salir".  
Muestra nombre y apellido de cada alumno utilizando un bucle `for`.

```csharp
// Solucion:
List<string> alumnos = new List<string>();
string alumno46 = "";

while (alumno46 != "salir")
{
    Console.Write("Ingresa el nombre del alumno (o 'salir' para terminar): ");
    string nombre = Console.ReadLine();
    if (nombre == "salir")
    {
        alumno46 = "salir";
        break;
    }

    Console.Write("Ingresa el apellido del alumno: ");
    string apellido = Console.ReadLine();

    alumno46 = $"{nombre} {apellido}";
    alumnos.Add(alumno46);
}

for (int i = 0; i < alumnos.Count; i++)
{
    Console.WriteLine(alumnos[i]);
}
```

### Ejercicio 47

Pide al usuario que ingrese tantos nombres como quiera.  
Guarda cada nombre en un array.  
Termina el bucle cuando el usuario ingrese la palabra "salir".  
Pide al usuario que ingrese el nombre de un alumno.  
- Muestra "El alumno está en la lista" si el nombre ingresado está en el array.  
- Muestra "El alumno no está en la lista" si no lo está.

```csharp
// Solucion:
List<string> nombres47 = new List<string>();
string nombre47 = "";

while (nombre47 != "salir")
{
    Console.Write("Ingresa un nombre (o 'salir' para terminar): ");
    nombre47 = Console.ReadLine();
    if (nombre47 != "salir")
    {
        nombres47.Add(nombre47);
    }
}

Console.Write("Ingresa el nombre de un alumno a buscar: ");
string alumnoBuscado = Console.ReadLine();

bool estaEnLaLista = false;

for (int i = 0; i < nombres47.Count; i++)
{
    if (nombres47[i] == alumnoBuscado)
    {
        estaEnLaLista = true;
        break;
    }
}

if (estaEnLaLista)
{
    Console.WriteLine("El alumno está en la lista");
}
else
{
    Console.WriteLine("El alumno no está en la lista");
}
```

### Ejercicio 48

Pide al usuario que ingrese tantos planetas y distancias como quiera.  
Del planeta pide el nombre, y la distancia es un número entero.  
Guarda cada planeta y distancia en un array.  
Termina el bucle cuando el usuario ingrese la palabra "salir".  
Muestra una cadena de texto donde aparezca el nombre de cada planeta ingresado, seguido de tantos puntos como la distancia ingresada.  
Ejemplo: `"Terra............Zolara.......Nexus.........."`

```csharp
// Solucion:
List<(string planeta, int distancia)> planetas = new List<(string, int)>();

string nombrePlaneta = "";
while (nombrePlaneta != "salir")
{
    Console.Write("Ingresa el nombre de un planeta (o 'salir' para terminar): ");
    nombrePlaneta = Console.ReadLine();
    if (nombrePlaneta.ToLower() != "salir")
    {
        Console.Write($"Ingresa la distancia de {nombrePlaneta}: ");
        int distancia = int.Parse(Console.ReadLine());
        planetas.Add((nombrePlaneta, distancia));
    }
}

string cadena = "";
foreach (var (planeta, dist) in planetas)
{
    cadena += planeta;
    for (int j = 0; j < dist; j++)
    {
        cadena += ".";
    }
}

Console.WriteLine(cadena);
```

# Ejercicios de programación en JavaScript Repartido 2

## Funciones

### Ejercicio 49

Crea una función llamada `saludar` que muestre un mensaje con "Hola Mundo!".

```csharp
static void Saludar()
{
    Console.WriteLine("Hola Mundo!");
}

// Uso:
Saludar();
```

### Ejercicio 50

Crea una función llamada `saludar` que reciba un nombre como argumento y muestre un mensaje con "Hola " seguido del nombre.

```csharp
static void Saludar(string nombre)
{
    Console.WriteLine("Hola " + nombre);
}

// Uso:
Console.Write("Ingresa tu nombre: ");
string nombre50 = Console.ReadLine();
Saludar(nombre50);
```

### Ejercicio 51

Crea una función llamada `saludar` que reciba un nombre y un apellido como argumentos y muestre un mensaje con "Hola " seguido del nombre y apellido.

```csharp
static void Saludar(string nombre, string apellido)
{
    Console.WriteLine($"Hola {nombre} {apellido}");
}

// Uso:
Console.Write("Ingresa tu nombre: ");
string nombre51 = Console.ReadLine();
Console.Write("Ingresa tu apellido: ");
string apellido51 = Console.ReadLine();
Saludar(nombre51, apellido51);
```

### Ejercicio 52

Crea una función llamada `calcularEdad` que reciba un año de nacimiento y muestre un mensaje con la edad que cumpliría en el año actual.

```csharp
static void CalcularEdad(int anoDeNacimiento)
{
    int anoActual = DateTime.Now.Year; // O usar 2025 si lo prefieres fijo
    int edad = anoActual - anoDeNacimiento;
    Console.WriteLine($"Tendrías {edad} años en {anoActual}.");
}

// Uso:
Console.Write("Ingresa tu año de nacimiento: ");
int anio52 = int.Parse(Console.ReadLine());
CalcularEdad(anio52);
```

### Ejercicio 53

Crea una función llamada `calcularEdad` que reciba un año de nacimiento y un año actual como argumentos y muestre un mensaje con la edad que cumpliría en el año actual.

```csharp
static void CalcularEdad(int anoDeNacimiento, int anoActual)
{
    int edad = anoActual - anoDeNacimiento;
    Console.WriteLine($"Tendrías {edad} años en el año {anoActual}.");
}

// Uso:
Console.Write("Ingresa tu año de nacimiento: ");
int anioNac53 = int.Parse(Console.ReadLine());

Console.Write("Ingresa el año actual: ");
int anioAct53 = int.Parse(Console.ReadLine());

CalcularEdad(anioNac53, anioAct53);
```

### Ejercicio 54

Crea una función llamada `cambiarTextoAMinusculas` que reciba un texto como argumento y muestre un mensaje con el texto en minúsculas.

```csharp
static void CambiarTextoAMinusculas(string texto)
{
    Console.WriteLine(texto.ToLower());
}

// Uso:
Console.Write("Ingresa un texto: ");
string texto54 = Console.ReadLine();
CambiarTextoAMinusculas(texto54);
```

### Ejercicio 55

Crea una función llamada `cambiarTextoAMayusculas` que reciba un texto como argumento y muestre un mensaje con el texto en mayúsculas.

```csharp
static void CambiarTextoAMayusculas(string texto)
{
    Console.WriteLine(texto.ToUpper());
}

// Uso:
Console.Write("Ingresa un texto: ");
string texto55 = Console.ReadLine();
CambiarTextoAMayusculas(texto55);
```

### Ejercicio 56

Crea una función llamada `sumar` que reciba dos números como argumentos y muestre un mensaje con la suma de los dos números.

```csharp
static void Sumar(int a, int b)
{
    int resultado = a + b;
    Console.WriteLine($"La suma de {a} y {b} es: {resultado}");
}

// Uso:
Console.Write("Ingresa el primer número: ");
int n1_56 = int.Parse(Console.ReadLine());

Console.Write("Ingresa el segundo número: ");
int n2_56 = int.Parse(Console.ReadLine());

Sumar(n1_56, n2_56);
```

### Ejercicio 57

Crea una función llamada `sumar` que reciba dos números como argumentos y devuelva (`return`) la suma de los dos números.

```csharp
static int Sumar(int a, int b)
{
    return a + b;
}

// Uso:
Console.Write("Ingresa el primer número: ");
int n1_57 = int.Parse(Console.ReadLine());

Console.Write("Ingresa el segundo número: ");
int n2_57 = int.Parse(Console.ReadLine());

int resultado57 = Sumar(n1_57, n2_57);
Console.WriteLine($"La suma es: {resultado57}");
```

### Ejercicio 58

Crea una función llamada `identificarOperador` que recibe una cadena de texto con un signo de operador matemático (+, -, *, /) y muestra un mensaje con el nombre del operador.

```csharp
static void IdentificarOperador(string operador)
{
    switch (operador)
    {
        case "+":
            Console.WriteLine("El operador es 'suma'");
            break;
        case "-":
            Console.WriteLine("El operador es 'resta'");
            break;
        case "*":
            Console.WriteLine("El operador es 'multiplicación'");
            break;
        case "/":
            Console.WriteLine("El operador es 'división'");
            break;
        default:
            Console.WriteLine("Operador desconocido");
            break;
    }
}

// Uso:
Console.Write("Ingresa un signo de operador (+, -, *, /): ");
string op58 = Console.ReadLine();
IdentificarOperador(op58);
```

### Ejercicio 59

Crea una función llamada `agregarIVA` que recibe un precio como argumento y devuelve el precio con el IVA (23%) agregado.

```csharp
static double AgregarIVA(double precio)
{
    double iva = 0.23;
    return precio + (precio * iva);
}

// Uso:
Console.Write("Ingresa un precio: ");
double precio59 = double.Parse(Console.ReadLine());
double precioConIVA = AgregarIVA(precio59);
Console.WriteLine($"El precio con IVA es: {precioConIVA}");
```

### Ejercicio 60

Crea una función llamada `calcularAreaTriangulo` que recibe la base y la altura de un triángulo como argumentos y devuelve el área del triángulo.

```csharp
static double CalcularAreaTriangulo(double baseT, double altura)
{
    return (baseT * altura) / 2.0;
}

// Uso:
Console.Write("Ingresa la base del triángulo: ");
double baseTri = double.Parse(Console.ReadLine());

Console.Write("Ingresa la altura del triángulo: ");
double alturaTri = double.Parse(Console.ReadLine());

double areaTriangulo = CalcularAreaTriangulo(baseTri, alturaTri);
Console.WriteLine($"El área del triángulo es: {areaTriangulo}");
```

### Ejercicio 61

Crea una función llamada `calcularAreaRectangulo` que recibe el largo y el ancho de un rectángulo como argumentos y devuelve el área del rectángulo.

```csharp
static double CalcularAreaRectangulo(double largo, double ancho)
{
    return largo * ancho;
}

// Uso:
Console.Write("Ingresa el largo del rectángulo: ");
double largo61 = double.Parse(Console.ReadLine());

Console.Write("Ingresa el ancho del rectángulo: ");
double ancho61 = double.Parse(Console.ReadLine());

double areaRect = CalcularAreaRectangulo(largo61, ancho61);
Console.WriteLine($"El área del rectángulo es: {areaRect}");
```

### Ejercicio 62

Crea una función llamada `calcularAreaCirculo` que recibe el radio de un círculo como argumento y devuelve el área del círculo.

```csharp
static double CalcularAreaCirculo(double radio)
{
    return Math.PI * radio * radio;
}

// Uso:
Console.Write("Ingresa el radio del círculo: ");
double radio62 = double.Parse(Console.ReadLine());

double areaC = CalcularAreaCirculo(radio62);
Console.WriteLine($"El área del círculo es: {areaC}");
```

### Ejercicio 63

Crea una función llamada `calcularAreaCuadrado` que recibe el lado de un cuadrado como argumento y devuelve el área del cuadrado.

```csharp
static double CalcularAreaCuadrado(double lado)
{
    return lado * lado;
}

// Uso:
Console.Write("Ingresa el lado del cuadrado: ");
double lado63 = double.Parse(Console.ReadLine());

double areaCuadrado = CalcularAreaCuadrado(lado63);
Console.WriteLine($"El área del cuadrado es: {areaCuadrado}");
```

### Ejercicio 64

Crea una función llamada `validarSiEsNumero` que recibe un argumento y devuelve `true` si el argumento es un número y `false` si no lo es.

En C# podemos usar `double.TryParse`.

```csharp
static bool ValidarSiEsNumero(string valor)
{
    return double.TryParse(valor, out _);
}

// Uso:
Console.Write("Ingresa un valor para verificar si es número: ");
string valor64 = Console.ReadLine();
bool esNumero64 = ValidarSiEsNumero(valor64);
Console.WriteLine($"¿Es número?: {esNumero64}");
```

### Ejercicio 65

Crea una función llamada `validarCantidadDeCaracteres` que recibe un texto y un número de caracteres esperado.  
Devuelve `true` si el texto tiene la cantidad de caracteres esperada y `false` si no.

```csharp
static bool ValidarCantidadDeCaracteres(string texto, int numeroCaracteres)
{
    return texto.Length == numeroCaracteres;
}

// Uso:
Console.Write("Ingresa un texto: ");
string texto65 = Console.ReadLine();

Console.Write("Ingresa la cantidad de caracteres esperada: ");
int cant65 = int.Parse(Console.ReadLine());

bool esValido65 = ValidarCantidadDeCaracteres(texto65, cant65);
Console.WriteLine($"El texto tiene la cantidad esperada?: {esValido65}");
```

### Ejercicio 66

Crea una función llamada `devolverComoArray` que recibe tres argumentos y devuelve un array con los tres argumentos.

```csharp
static string[] DevolverComoArray(string arg1, string arg2, string arg3)
{
    return new string[] { arg1, arg2, arg3 };
}

// Uso:
Console.Write("Ingresa el primer valor: ");
string v1 = Console.ReadLine();

Console.Write("Ingresa el segundo valor: ");
string v2 = Console.ReadLine();

Console.Write("Ingresa el tercer valor: ");
string v3 = Console.ReadLine();

string[] resultado66 = DevolverComoArray(v1, v2, v3);

Console.WriteLine("El array resultante es:");
for (int i = 0; i < resultado66.Length; i++)
{
    Console.WriteLine(resultado66[i]);
}
```

### Ejercicio 67

Crea una función llamada `devolverNumeroAleatorio` que recibe un número como argumento y devuelve un número aleatorio entre 0 y el número ingresado.

```csharp
static double DevolverNumeroAleatorio(double max)
{
    // Retorna un número aleatorio entre 0 (incl.) y max (excl.)
    Random rnd = new Random();
    return rnd.NextDouble() * max;
}

// Uso:
Console.Write("Ingresa un número máximo: ");
double max67 = double.Parse(Console.ReadLine());
double aleatorio67 = DevolverNumeroAleatorio(max67);
Console.WriteLine($"Número aleatorio entre 0 y {max67}: {aleatorio67}");
```

### Ejercicio 68

Crea una función llamada `devolverArrayDeNumerosAleatorios` que recibe como argumento un número y devuelve un array con la cantidad de números aleatorios entre 0 y 255 que se indique.

```csharp
static int[] DevolverArrayDeNumerosAleatorios(int cantidad)
{
    int[] resultado = new int[cantidad];
    Random rnd = new Random();

    for (int i = 0; i < cantidad; i++)
    {
        // Número aleatorio entre 0 y 255
        resultado[i] = rnd.Next(0, 256);
    }

    return resultado;
}

// Uso:
Console.Write("Ingresa la cantidad de números aleatorios que deseas: ");
int cant68 = int.Parse(Console.ReadLine());

int[] arrAleatorios = DevolverArrayDeNumerosAleatorios(cant68);

Console.WriteLine("Los números aleatorios generados son:");
for (int i = 0; i < arrAleatorios.Length; i++)
{
    Console.WriteLine(arrAleatorios[i]);
}
```

