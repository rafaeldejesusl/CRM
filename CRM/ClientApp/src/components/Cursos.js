import React, { Component } from 'react';

export class Cursos extends Component {
  static displayName = Cursos.name;

  constructor(props) {
    super(props);
    this.state = { cursos: [], loading: true, name: "", professor: "" };
    this.handleChange = this.handleChange.bind(this);
    this.handleClick = this.handleClick.bind(this);
    this.handleDelete = this.handleDelete.bind(this);
  }
  
  componentDidMount() {
    this.populateData();
  }
  
  async handleDelete(e) {
    const id = e.target.id;
    await fetch(`api/cursos/${id}`, {
      method: "DELETE",
    });
    this.populateData();
  }
  
  static renderTable(cursos, handleDelete) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Id</th>
            <th>Nome</th>
            <th>Professor</th>
            <th>Excluir</th>
          </tr>
        </thead>
        <tbody>
          {cursos.map(curso =>
            <tr key={curso.cursoId}>
              <td>{curso.cursoId}</td>
              <td>{curso.cursoName}</td>
              <td>{curso.cursoProfessor}</td>
              <td>
                <button
                  type="button"
                  className="btn btn-danger btn-sm"
                  id={ curso.cursoId }
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
    const { name, professor } = this.state;
    const obj = {
      cursoName: name,
      cursoProfessor: professor,
    };
    await fetch('api/cursos', {
      method: "POST",
      headers: {
        'Content-type': 'application/json; charset=UTF-8'
      },
      body: JSON.stringify(obj)
    });
    this.populateData();
  }

  render() {
    const { name, professor } = this.state;
    let contents = this.state.loading
    ? <p><em>Loading...</em></p>
    : Cursos.renderTable(this.state.cursos, this.handleDelete);
    
    return (
      <div>
        <h1 id="tabelLabel" >Cursos</h1>
        <p>Nessa página é possível manipular as informações dos cursos</p>
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
            <label htmlFor='professor-input' className="form-label mx-1">
              Professor:
              <input
                type="text"
                className="form-control"
                id="professor-input"
                name="professor"
                value={ professor }
                onChange={ this.handleChange }
              />
            </label>
          </div>
          <button type="submit" className="btn btn-primary my-3 mx-1" onClick={ this.handleClick }>
            Criar Curso
          </button>
        </form>
        {contents}
      </div>
    );
  }

  async populateData() {
    const response = await fetch('api/cursos', { method: "GET" });
    const data = await response.json();
    this.setState({ cursos: data, loading: false });
  }
}
