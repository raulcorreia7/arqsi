export enum RestricaoEnum {
    Caber = 1,
    Material,
    Obrigatoria,
    Opcional,
    Ocupacao
};

export const RestricaoMap = new Map<number, string>(
    [
        [RestricaoEnum.Caber, 'Restricao Caber'],
        [RestricaoEnum.Material, 'Restricao Material'],
        [RestricaoEnum.Obrigatoria, 'Restricao Obrigatoria'],
        [RestricaoEnum.Opcional, 'Restricao Opcional'],
        [RestricaoEnum.Ocupacao, 'Restricao Ocupacao']

    ]
);