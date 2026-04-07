# Programacion 3 2026
## Clase 6

# Hoy

- Cierre **Unidad 3:** refactorizacion y documentacion
- Integracion del flujo: **compilar**, **`dotnet test`**, **documentar** con apoyo de IA
- Actividades del programa: codigo **sucio** + **XML / README**

Ayer trabajamos **tests con xUnit y AAA**. Hoy usamos el mismo criterio de revision humana para **refactors** y para **documentacion** que no mienta ni desactualice el codigo.

---

# Venimos de aca

- Tests generados con IA, ejecutados con **`dotnet test`**
- Revision de **pertinencia** y bordes, no solo "que pase"

Hoy el riesgo principal es **cambiar comportamiento sin notarlo**: el refactor debe quedar respaldado por tests, los que ya tienen o los que agreguen hoy.

---

# Ejemplo de referencia: Calculadora + tests

Para demostrar **refactor sobre codigo que ya tiene red de seguridad**, se puede partir de la misma **Calculadora** y **CalculadoraTests** del lunes.

- Texto completo con mas casos y notas de discusion: `2026/archivos/ejemplo_calculadora_y_tests_xunit.md`
- Flujo sugerido: `dotnet test` en verde → pedir a la IA un refactor de nombres o extraccion de metodo privado **sin cambiar firmas publicas** → `dotnet test` otra vez.

---

# Unidad 3
## Refactorizacion asistida por IA

**Que pedir, por ejemplo:**

- Extraer metodo, renombrar con intencion clara
- Simplificar condiciones anidadas, eliminar duplicacion obvia
- Agrupar responsabilidades en metodos mas pequenos, sin introducir patrones o capas que el curso aun no haya visto

**Como revisar:**

- Leer el **diff** completo antes de aceptar
- **`dotnet build`** y **`dotnet test`** inmediatamente despues
- Si un test falla: o el refactor rompio algo, o el test estaba mal — en ambos casos **ustedes** deciden

---

# Refactor
## Prompts utiles

- "Refactoriza **sin cambiar** el comportamiento observable; conserva firmas publicas salvo que indique lo contrario."
- "Mostrame el diff por pasos: primero renombres, luego extracciones."
- "No introduzcas nuevas dependencias ni patrones que no esten en el proyecto."

Si la IA **mezcla** refactor con cambio funcional, conviene pedir **pasos separados** o revertir y acotar el pedido.

---

# Actividad sugerida
## Fragmento "sucio"

- El docente entrega un fragmento con nombres pobres, metodos largos o duplicacion
- Refactor con IA + revision humana
- **Entrega breve** en un parrafo o lista: que cambios se hicieron y **por que**, por ejemplo legibilidad, DRY o menor riesgo

Eso es material alineado a la **documentacion del proceso** que pedira el trabajo integrador en `ia_docs`.

---

# Documentacion tecnica con IA

**Comentarios XML** en miembros publicos visibles:

- `<summary>`, `<param>`, `<returns>`, `<exception>` cuando aplique
- Generar con IA y **verificar** contra la implementacion real: parametros, null, excepciones

**README del proyecto:**

- Como compilar, como ejecutar tests, estructura minima del repo
- Evitar prometer scripts o comandos que no existen

**Contratos:** si hay API publica con DTOs o endpoints, la descripcion debe coincidir con el codigo.

---

# Documentacion
## Revision

- ¿El XML **contradice** el codigo en tipos, null o excepciones?
- ¿El README tiene **comandos copy-paste** que funcionan en .NET 8?
- ¿Hay ruido: comentarios que repiten lo obvio sin agregar valor?

Menos texto **correcto** es mejor que mucho texto **generico** pegado por la IA.

---

# Ciclo integrado

```text
editar o pedir a la IA → dotnet build → dotnet test → actualizar docs si cambio el contrato
```

La documentacion no es solo "al final del proyecto": si cambian una firma publica, el XML o el README deben **seguir** o quedan como deuda.

---

# Conexion con el integrador

- La letra del trabajo integrador pide carpeta **`ia_docs`**: configuracion del IDE, rules/skills si los usan, **planes numerados** de implementacion
- En la Unidad 3 practicaron tests, refactors y docs con revision; es el mismo **habito** que necesitaran para sostener el proyecto en equipo

---

# Actividades de la unidad

- Modulo C# + tests con IA, ejecutar y corregir, como en la clase del lunes
- Refactorizar codigo **sucio** con IA y **documentar** cambios y criterios
- Redactar **XML + README** para un mini proyecto con IA y comprobar precision

Si falta tiempo, priorizar **un** flujo completo: refactor, tests verdes y README corto, antes que muchos fragmentos sueltos.

---

# Cierre Unidad 3

- **Tests:** pertinencia y bordes; `dotnet test` en cada cambio serio
- **Refactor:** diff revisado, comportamiento preservado, sin magia de dependencias nuevas
- **Docs:** XML y README alineados al codigo; revision humana obligatoria

---

# Lo que sigue

- **Unidad 4:** comparacion entre modelos, checklist de calidad, cierre del bloque IA
- Seguiremos usando IA en todo el curso; las proximas unidades ya mezclan **dominio** .NET, EF y MVC con el mismo criterio de revision
