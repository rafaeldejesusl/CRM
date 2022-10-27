import { Cursos } from "./components/Cursos";
import { Candidatos } from "./components/Candidatos";
import { Inscricoes } from "./components/Inscricoes";
import { Home } from "./components/Home";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/cursos',
    element: <Cursos />
  },
  {
    path: '/candidatos',
    element: <Candidatos />
  },
  {
    path: '/inscricoes',
    element: <Inscricoes />
  }
];

export default AppRoutes;
