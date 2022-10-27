import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <div>
        <h1>Bem-vindo!</h1>
        <p>Nessa aplicação é possível gerenciar as seguintes informações:</p>
        <ul>
          <li>
            Cursos
          </li>
          <li>
            Candidatos
          </li>
          <li>
            Inscrições
          </li>
        </ul>
        <p>Escolha a página que deseja acessar através do menu de navegação na barra superior.</p>
      </div>
    );
  }
}
