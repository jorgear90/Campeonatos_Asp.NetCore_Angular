Implementación del EncryptionService

Para el traspaso de IDs al editar un equipo. 

🔐 EncryptionService (Angular + AES con CryptoJS)
📄 Descripción

Este servicio provee un sistema de cifrado y descifrado de texto utilizando el algoritmo AES (Advanced Encryption Standard) a través de la librería crypto-js.

Su objetivo principal es proteger datos sensibles en el Frontend, como identificadores o información temporal almacenada en sessionStorage o localStorage.

⚙️ Instalación

Ejecutar en la raíz del proyecto Angular:

npm install crypto-js
npm install --save-dev @types/crypto-js

📂 Creación del servicio

En la terminal ejecuta lo siguiente: 

ng g s core/services/encryption/encryption

💻 Código del servicio:

import { Injectable } from '@angular/core';
import * as CryptoJS from 'crypto-js';

@Injectable({
  providedIn: 'root'
})
export class EncryptionService {

  //Clave privada (puedes cambiarla, pero mantenla fija en el front)
  private secretKey = 'TienesQuePonerTuClaveAquí';

  // Cifrar texto
  encrypt(data: string): string {
    return CryptoJS.AES.encrypt(data, this.secretKey).toString();
  }

  // Descifrar texto
  decrypt(cipherText: string): string {
    try {
      const bytes = CryptoJS.AES.decrypt(cipherText, this.secretKey);
      return bytes.toString(CryptoJS.enc.Utf8);
    } catch {
      return '';
    }
  }
}