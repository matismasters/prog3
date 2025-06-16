const fs = require('fs');

// Lista de caracteres especiales de GIFT que necesitan ser escapados
const caracteresEspeciales = ['~', '=', '{', '}', '#', ':', '%', '\\'];

// Función para escapar caracteres especiales en preguntas y respuestas
function escaparTexto(texto) {
  let resultado = texto;
  caracteresEspeciales.forEach(caracter => {
    const regex = new RegExp(`\\${caracter}`, 'g');
    resultado = resultado.replace(regex, `\\${caracter}`);
  });
  return resultado;
}

// Cargar el archivo JSON
const archivo = 'preguntas.json';
let contenidoJSON;

try {
  const datos = fs.readFileSync(archivo, 'utf8');
  contenidoJSON = JSON.parse(datos);
} catch (err) {
  console.error('Error al leer o parsear el archivo JSON:', err.message);
  process.exit(1);
}

const preguntas = contenidoJSON.preguntas;
let salidaGIFT = '';

// Procesar cada pregunta
preguntas.forEach((item, index) => {
  const textoPregunta = escaparTexto(item.pregunta.trim());
  let bloqueRespuestas = '';

  item.respuestas.forEach(resp => {
    const porcentaje = parseInt(resp.porcentajeCorrecto, 10);
    const textoRespuesta = escaparTexto(resp.textoRespuesta.trim());

    bloqueRespuestas += `~%${porcentaje}% ${textoRespuesta}\n`;
  });

  salidaGIFT += `${textoPregunta} {\n${bloqueRespuestas}}\n\n`;
});

// Guardar archivo .gift
const archivoSalida = 'preguntas.gift';
fs.writeFileSync(archivoSalida, salidaGIFT, 'utf8');
console.log(`Archivo generado correctamente: ${archivoSalida}`);
// Fin del script
