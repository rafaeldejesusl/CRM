import React, { Component } from 'react';

export class Inscricoes extends Component {
  static displayName = Inscricoes.name;

  constructor(props) {
    super(props);
    this.state = { inscricoes: [], loading: true, candidato: "", curso: "" };
    this.handleChange = this.handleChange.bind(this);
    this.handleClick = this.handleClick.bind(this);
    this.handleDelete = this.handleDelete.bind(this);
  }
  
  componentDidMount() {
    this.populateData();
  }
  
  async handleDelete(e) {
    const id = e.target.id;
    await fetch(`api/inscricoes/${id}`, {
      method: "DELETE",
    });
    this.populateData();
  }
  
  static renderTable(inscricoes, handleDelete) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Id</th>
            <th>Candidato</th>
            <th>Curso</th>
            <th>Excluir</th>
          </tr>
        </thead>
        <tbody>
          {inscricoes.map(inscricao =>
            <tr key={inscricao.inscricaoId}>
              <td>{inscricao.inscricaoId}</td>
              <td>{inscricao.candidato.candidatoName}</td>
              <td>{inscricao.curso.cursoName}</td>
              <td>
                <button
                  type="button"
                  className="btn btn-danger btn-sm"
                  id={ inscricao.inscricaoId }
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
    const { candidato, curso } = this.state;
    const obj = {
      CandidatoId: candidato,
      CursoId: curso,
    };
    const cursoResponse = await fetch(`api/cursos/${curso}`, { method: "GET" });
    const candidatoResponse = await fetch(`api/candidatos/${candidato}`, { method: "GET" });
    if (cursoResponse.status === 204 || candidatoResponse.status === 204) {
      window.alert("Curso ou Candidato Inválido");
      return;
    }
    await fetch('api/inscricoes', {
      method: "POST",
      headers: {
        'Content-type': 'application/json; charset=UTF-8'
      },
      body: JSON.stringify(obj)
    });
    this.populateData();
  }

  render() {
    const { candidato, curso } = this.state;
    let contents = this.state.loading
    ? <p><em>Loading...</em></p>
    : Inscricoes.renderTable(this.state.inscricoes, this.handleDelete);
    
    return (
      <div>
        <h1 id="tabelLabel" >Inscrições</h1>
        <p>Nessa página é possível manipular as informações das Inscrições</p>
        <form>
          <div>
            <label htmlFor='candidato-input' className="form-label mx-1">
              Id do Candidato:
              <input
                type="text"
                className="form-control"
                id="candidato-input"
                name="candidato"
                value={ candidato }
                onChange={ this.handleChange }
              />
            </label>
            <label htmlFor='curso-input' className="form-label mx-1">
              Id do Curso:
              <input
                type="text"
                className="form-control"
                id="curso-input"
                name="curso"
                value={ curso }
                onChange={ this.handleChange }
              />
            </label>
          </div>
          <button type="submit" className="btn btn-primary my-3 mx-1" onClick={ this.handleClick }>
            Criar Inscrição
          </button>
        </form>
        {contents}
      </div>
    );
  }

  async populateData() {
    const response = await fetch('api/inscricoes', { method: "GET" });
    const data = await response.json();
    this.setState({ inscricoes: data, loading: false });
  }
}
