export interface IColaborador {
    id: string,
    matricula: string,
    nome: string,
    cpf: string,
    rg: string,
    dataNascimento: string,
    tipo: number,
    dataAdmissao?: string | null;
    valorContribuicao?: number | null;
}