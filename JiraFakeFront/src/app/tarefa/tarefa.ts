export interface Tarefa {
  id: string;
  nome: string;
  descricao: string;
  status: any;
  dataCadastro: Date;
  subTarefa: SubTarefa[];
}

export interface SubTarefa {
  id: string;
  nome: string;
  descricao: string;
  status: any;
  dataCadastro: Date;
  tarefaId: string;
}
