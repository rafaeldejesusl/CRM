import React, { Component } from 'react';

export class Candidatos extends Component {
  static displayName = Candidatos.name;

  constructor(props) {
    super(props);
    this.state = { candidatos: [], loading: true, name: "", email: "", cpf:"" };
    this.handleChange = this.handleChange.bind(this);
    this.handleClick = this.handleClick.bind(this);
    this.handleDelete = this.handleDelete.bind(this);
  }
  
  componentDidMount() {
    this.populateData();
  }
  
  async handleDelete(e) {
    const id = e.target.id;
    await fetch(`api/candidatos/${id}`, {
      method: "DELETE",
    });
    this.populateData();
  }
  
  static renderTable(candidatos, handleDelete) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Id</th>
            <th>Nome</th>
            <th>Email</th>
            <th>CPF</th>
            <th>Excluir</th>
          </tr>
        </thead>
        <tbody>
          {candidatos.map(candidato =>
            <tr key={candidato.candidatoId}>
              <td>{candidato.candidatoId}</td>
              <td>{candidato.candidatoName}</td>
              <td>{candidato.candidatoEmail}</td>
              <td>{candidato.candidatoCpf}</td>
              <td>
                <button
                  type="button"
                  className="btn btn-danger btn-sm"
                  id={ candidato.candidatoId }
                  onClick={ handleDelete }
                  >
                  X
                </button>
              </td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }
  
  handleChange({ target }) {
    const { name } = target;
    const value = target.type === 'checkbox' ? target.checked : target.value;
    this.setState({
      [name]: value,
    });
  }
  
  async handleClick(e) {
    e.preventDefault();
    const { name, email, cpf } = this.state;
    const obj = {
      candidatoName: name,
      candidatoEmail: email,
      candidatoCpf: cpf
    };
    await fetch('api/candidatos', {
      method: "POST",
      headers: {
        'Content-type': 'application/json; charset=UTF-8'
      },
      body: JSON.stringify(obj)
    });
    this.populateData();
  }

  render() {
    const { name, email, cpf } = this.state;
    let contents = this.state.loading
    ? <p><em>Loading...</em></p>
    : Candidatos.renderTable(this.state.candidatos, this.handleDelete);
    
    return (
      <div>
        <h1 id="tabelLabel" >Candidatos</h1>
        <p>Nessa página é possível manipular as informações dos candidatos</p>
        <form>
          <div>
            <label htmlFor='name-input' className="form-label mx-1">
              Nome:
              <input
                type="text"
                className="form-control"
                id="name-input"
                name="name"
                value={ name }
                onChange={ this.handleChange }
              />
            </label>
            <label htmlFor='email-input' className="form-label mx-1">
              Email:
              <input
                type="text"
                className="form-control"
                id="email-input"
                name="email"
                value={ email }
                onChange={ this.handleChange }
              />
            </label>
            <label htmlFor='cpf-input' className="form-label mx-1">
              CPF:
              <input
                type="text"
                className="form-control"
                id="cpf-input"
                name="cpf"
                value={ cpf }
                onChange={ this.handleChange }
              />
            </label>
          </div>
          <button type="submit" className="btn btn-primary my-3 mx-1" onClick={ this.handleClick }>
            Criar Candidato
          </button>
        </form>
        {contents}
      </div>
    );
  }

  async populateData() {
    const response = await fetch('api/candidatos', { method: "GET" });
    const data = await response.json();
    this.setState({ candidatos: data, loading: false });
  }
}
