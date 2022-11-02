﻿using CSharpFunctionalExtensions;
using Dapper;
using Framework.Core.Base.ModelEntity;
using Framework.Core.Exceptions;
using Framework.Core.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDVI.Repository.AfasDtos;
using VDVI.Repository.Dtos.AfasDtos;
using VDVI.Repository.Dtos.AfasDtos.AfasCommonDtos;
using VDVI.Services.AfasInterfaces;
using VDVI.Services.Services.BaseService;

namespace VDVI.Services.AfasServices
{
    public class DmfFinancieleMutatesService : AfasBaseService, IdmfFinancieleMutatiesService
    {
        private readonly IdmFFinancieleMutationService _dmFFinancieleMutationService;
        List<DMFAdministratiesDto> administratiesdto = new List<DMFAdministratiesDto>();
        List<DMFFinancieleMutatiesDto> financielemutatiesDto = new List<DMFFinancieleMutatiesDto>();

        DMFFinancieleMutatiesDto[]? financielemutatiesAA;
        DMFFinancieleMutatiesDto[]? financielemutatiesAC;
        DMFFinancieleMutatiesDto[]? financielemutatiesAD;
        DMFFinancieleMutatiesDto[]? financielemutatiesAE;

        public DmfFinancieleMutatesService(IdmFFinancieleMutationService dmFFinancieleMutationService)
        {
            _dmFFinancieleMutationService = dmFFinancieleMutationService;
        }

        public async Task DmfFinancieleMutatiesServiceExistingAsync(string lastmutationdatetime, string lastdayofthisyear, AfasCrenditalsDto getConnector)
        {
            DMFBoekingsdagenMutatiesDto[]? boekingsdagenMutatiesAA;
            DMFBoekingsdagenMutatiesDto[]? boekingsdagenMutatiesAC;
            DMFBoekingsdagenMutatiesDto[]? boekingsdagenMutatiesAD;
            DMFBoekingsdagenMutatiesDto[]? boekingsdagenMutatiesAE;

            boekingsdagenMutatiesAA = await getConnector.clientAA.Query<DMFBoekingsdagenMutatiesDto>()
                                            .WhereGreaterThen(x => x.Datum_gewijzigd, lastmutationdatetime)
                                            .WhereLessOrEqual(x => x.Datum_boeking, lastdayofthisyear)
                                            .Skip(-1)
                                            .Take(-1)
                                            .GetAsync();
            if (boekingsdagenMutatiesAA.Length > 0)
            {
                foreach (var item in boekingsdagenMutatiesAA)
                {
                    DateTime dt = item.Datum_boeking.Value;
                    string bookingdate = String.Format("{0:yyyy-MM-ddT00:00:00.000}", dt);

                    //get financial mutations for selected booking date
                    financielemutatiesAA = await getConnector.clientAA.Query<DMFFinancieleMutatiesDto>()
                    .WhereEquals(x => x.Datum_boeking, bookingdate)
                    .Skip(-1)
                    .Take(-1)
                    .GetAsync();
                }
                if (financielemutatiesAA.Length > 0) FormatSummaryObjectAA(financielemutatiesAA.ToList(), financielemutatiesDto);

            }

            boekingsdagenMutatiesAC = await getConnector.clientAC.Query<DMFBoekingsdagenMutatiesDto>()
                                           .WhereGreaterThen(x => x.Datum_gewijzigd, lastmutationdatetime)
                                           .WhereLessOrEqual(x => x.Datum_boeking, lastdayofthisyear)
                                           .Skip(-1)
                                           .Take(-1)
                                           .GetAsync();
            if (boekingsdagenMutatiesAC.Length > 0)
            {
                foreach (var item in boekingsdagenMutatiesAC)
                {
                    DateTime dt = item.Datum_boeking.Value;
                    string bookingdate = String.Format("{0:yyyy-MM-ddT00:00:00.000}", dt);

                    //get financial mutations for selected booking date
                    financielemutatiesAC = await getConnector.clientAC.Query<DMFFinancieleMutatiesDto>()
                    .WhereEquals(x => x.Datum_boeking, bookingdate)
                    .Skip(-1)
                    .Take(-1)
                    .GetAsync();
                }
                if (financielemutatiesAC.Length > 0) FormatSummaryObjectAA(financielemutatiesAC.ToList(), financielemutatiesDto);


            }

            boekingsdagenMutatiesAD = await getConnector.clientAD.Query<DMFBoekingsdagenMutatiesDto>()
                                          .WhereGreaterThen(x => x.Datum_gewijzigd, lastmutationdatetime)
                                          .WhereLessOrEqual(x => x.Datum_boeking, lastdayofthisyear)
                                          .Skip(-1)
                                          .Take(-1)
                                          .OrderBy(x => x.Datum_boeking)
                                          .GetAsync();
            if (boekingsdagenMutatiesAD.Length > 0)
            {
                foreach (var item in boekingsdagenMutatiesAD)
                {
                    DateTime dt = item.Datum_boeking.Value;
                    string bookingdate = String.Format("{0:yyyy-MM-ddT00:00:00.000}", dt);

                    //get financial mutations for selected booking date
                    financielemutatiesAD = await getConnector.clientAD.Query<DMFFinancieleMutatiesDto>()
                    .WhereEquals(x => x.Datum_boeking, bookingdate)
                    .Skip(-1)
                    .Take(-1)
                    .GetAsync();
                }

                if (financielemutatiesAD.Length > 0) FormatSummaryObjectAD(financielemutatiesAD.ToList(), financielemutatiesDto);
            }

            boekingsdagenMutatiesAE = await getConnector.clientAE.Query<DMFBoekingsdagenMutatiesDto>()
                                         .WhereGreaterThen(x => x.Datum_gewijzigd, lastmutationdatetime)
                                         .WhereLessOrEqual(x => x.Datum_boeking, lastdayofthisyear)
                                         .Skip(-1)
                                         .Take(-1)
                                         .GetAsync();
            if (boekingsdagenMutatiesAE.Length > 0)
            {
                foreach (var item in boekingsdagenMutatiesAE)
                {
                    DateTime dt = item.Datum_boeking.Value;
                    string bookingdate = String.Format("{0:yyyy-MM-ddT00:00:00.000}", dt);

                    //get financial mutations for selected booking date
                    financielemutatiesAE = await getConnector.clientAE.Query<DMFFinancieleMutatiesDto>()
                    .WhereEquals(x => x.Datum_boeking, bookingdate)
                    .Skip(-1)
                    .Take(-1)
                    .GetAsync();
                }
                if (financielemutatiesAE.Length > 0) FormatSummaryObjectAE(financielemutatiesAE.ToList(), financielemutatiesDto);

            }
        }

        public async Task DmfFinancieleMutatiesServiceInitial(int year, int code, AfasCrenditalsDto getConnector)
        {


            financielemutatiesAA = await getConnector.clientAA.Query<DMFFinancieleMutatiesDto>()
                                                                .WhereEquals(x => x.Administratie_code, code.ToString())
                                                                .WhereEquals(x => x.Jaar, year.ToString())
                                                                .Skip(-1)
                                                                .Take(-1)
                                                                .GetAsync();
            if (financielemutatiesAA.Length > 0) FormatSummaryObjectAA(financielemutatiesAA.ToList(), financielemutatiesDto);

            financielemutatiesAC = await getConnector.clientAC.Query<DMFFinancieleMutatiesDto>()
                                                                .WhereEquals(x => x.Administratie_code, code.ToString())
                                                                .WhereEquals(x => x.Jaar, year.ToString())
                                                                .Skip(-1)
                                                                .Take(-1)
                                                                .GetAsync();
            if (financielemutatiesAC.Length > 0) FormatSummaryObjectAC(financielemutatiesAC.ToList(), financielemutatiesDto);

            financielemutatiesAD = await getConnector.clientAD.Query<DMFFinancieleMutatiesDto>()
                                                                .WhereEquals(x => x.Administratie_code, code.ToString())
                                                                .WhereEquals(x => x.Jaar, year.ToString())
                                                                .Skip(-1)
                                                                .Take(-1)
                                                                .GetAsync();
            if (financielemutatiesAD.Length > 0) FormatSummaryObjectAD(financielemutatiesAD.ToList(), financielemutatiesDto);

            financielemutatiesAE = await getConnector.clientAE.Query<DMFFinancieleMutatiesDto>()
                                                                .WhereEquals(x => x.Administratie_code, code.ToString())
                                                                .WhereEquals(x => x.Jaar, year.ToString())
                                                                .Skip(-1)
                                                                .Take(-1)
                                                                .GetAsync();
            if (financielemutatiesAE.Length > 0) FormatSummaryObjectAE(financielemutatiesAE.ToList(), financielemutatiesDto);


        }
        public async Task<Result<PrometheusResponse>> DmfFinancieleMutatiesServiceAsync(DateTime startDate)
        {
            int startBusinessYear = startDate.Year;
            int currentYear = DateTime.UtcNow.Year;
            AfasCrenditalsDto getConnector = GetAfmaConnectors();

            var res = await _dmFFinancieleMutationService.GetInitialRecordAndLastRecordDatetime();
            var formatres = (MutationDto)res.Value.Data;


            return await TryCatchExtension.ExecuteAndHandleErrorAsync(
                async () =>
                {
                    //Load Initial Records  
                    if (formatres.IsInitialRecord == false)
                    {
                        var dto = await AdministrativeList();
                        var aa = dto._AA.Select(x => x.Administratie_code);
                        var ac = dto._AC.Select(x => x.Administratie_code);
                        var ad = dto._AD.Select(x => x.Administratie_code);
                        var ae = dto._AE.Select(x => x.Administratie_code);
                        var administratiesCode = aa.Concat(ac).Concat(ad).Concat(ae).Distinct().ToList().OrderBy(x => x); 
                        
                        for (int year = startBusinessYear; year <= currentYear; year++)
                        {  
                            foreach (var code in administratiesCode)
                            {
                                await DmfFinancieleMutatiesServiceInitial(year, code, getConnector);
                                // DB operation
                                if (financielemutatiesDto.Count > 0)
                                {
                                    await _dmFFinancieleMutationService.BulkInsertWithProcAsync(financielemutatiesDto, true);
                                    financielemutatiesDto.Clear();
                                }
                            } 
                        }
                    }
                    //Get Changes Record  
                    else
                    {
                        //last mutation datetime (example 18-10-2022 12:30:05)
                        DateTime? dt = formatres.LastRecordDate;
                        string lastmutationdatetime = String.Format("{0:yyyy-MM-ddTHH:mm:ss}", dt); //check this date in the database 
                        string lastdayofthisyear = DateTime.Now.Year.ToString() + "-12-31T00:00:00.000"; //last day of current year

                        await DmfFinancieleMutatiesServiceExistingAsync(lastmutationdatetime, lastdayofthisyear, getConnector);
                        // DB operation
                        if (financielemutatiesDto.Count > 0)
                        {
                            await _dmFFinancieleMutationService.BulkInsertWithProcAsync(financielemutatiesDto, false);
                            financielemutatiesDto.Clear();
                        }
                    } 

                    return PrometheusResponse.Success("", "Data retrieval is successful");
                },
                exception => new TryCatchExtensionResult<Result<PrometheusResponse>>
                {
                    DefaultResult = PrometheusResponse.Failure($"Error message: {exception.Message}. Details: {ExceptionExtension.GetExceptionDetailMessage(exception)}"),
                    RethrowException = false
                }
            );

        }
        private void FormatSummaryObjectAA(List<DMFFinancieleMutatiesDto> _financielemutatiesAA, List<DMFFinancieleMutatiesDto> _financielemutatiesDto)
        {

            if (_financielemutatiesAA.Count > 0)
            {
                _financielemutatiesAA.ForEach(a => a.Omgeving_code = "AA");
                _financielemutatiesDto.AddRange(_financielemutatiesAA);
            }

        }
        private void FormatSummaryObjectAC(List<DMFFinancieleMutatiesDto> _financielemutatiesAC, List<DMFFinancieleMutatiesDto> _financielemutatiesDto)
        {

            if (_financielemutatiesAC.Count > 0)
            {
                _financielemutatiesAC.ForEach(a => a.Omgeving_code = "AC");
                _financielemutatiesDto.AddRange(_financielemutatiesAC);
            }

        }
        private void FormatSummaryObjectAD(List<DMFFinancieleMutatiesDto> _financielemutatiesAD, List<DMFFinancieleMutatiesDto> _financielemutatiesDto)
        {

            if (_financielemutatiesAD.Count > 0)
            {
                _financielemutatiesAD.ForEach(a => a.Omgeving_code = "AD");
                _financielemutatiesDto.AddRange(_financielemutatiesAD);
            }

        }
        private void FormatSummaryObjectAE(List<DMFFinancieleMutatiesDto> _financielemutatiesAE, List<DMFFinancieleMutatiesDto> _financielemutatiesDto)
        {

            if (_financielemutatiesAE.Count > 0)
            {
                _financielemutatiesAE.ForEach(a => a.Omgeving_code = "AE");
                _financielemutatiesDto.AddRange(_financielemutatiesAE);
            }

        }


    }
}
