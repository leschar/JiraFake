export interface Tarefa {
  id: string;
  nome: string;
  descricao: string;
  status: string;
  dataCadastro: Date;
  subTarefa: SubTarefa[];
}

export interface SubTarefa {
  id: string;
  nome: string;
  descricao: string;
  status: string;
  dataCadastro: Date;
  tarefaId: string;
}
