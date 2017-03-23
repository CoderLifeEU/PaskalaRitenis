using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using PaskalaRitenis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaskalaRitenis.CustomManagers
{
    public class XlsManager
    {

        HSSFWorkbook hssfworkbook;
        ISheet sheet;
        ICellStyle header;

        public void CreateWorkbook()
        {
            hssfworkbook = new HSSFWorkbook();
            sheet = hssfworkbook.CreateSheet();

            header = hssfworkbook.CreateCellStyle();
            header.WrapText = true;

            IFont font = hssfworkbook.CreateFont();
            font.IsBold = true;
            header.SetFont(font);
        }

        public HSSFWorkbook CreateRegistrationXLS(List<string> headers,List<Registration> data)
        {
            CreateWorkbook();
            AddHeader(headers);
            AddRegistrations(data);

            return hssfworkbook;
        }

        public void AddHeader(List<String> data)
        {
            var row = sheet.CreateRow(0);

            for(int i=0;i<data.Count;i++)
            {
                var cell = row.CreateCell(i);
                cell.CellStyle = header;
                cell.SetCellValue(data.ElementAt(i));
            }
        }

        public void AddRegistrations(List<Registration> data)
        {
            int rowIndex = 1;
            for (int i = 0; i < data.Count; i++)
            {
                int cellIndex = 0;
                var row = sheet.CreateRow(rowIndex);
                var cell = row.CreateCell(cellIndex);
                cell.SetCellValue(data.ElementAt(i).RegCode);
                cellIndex++;

                cell = row.CreateCell(cellIndex);
                cell.SetCellValue(data.ElementAt(i).Vards);
                cellIndex++;

                cell = row.CreateCell(cellIndex);
                cell.SetCellValue(data.ElementAt(i).Uzvards);
                cellIndex++;

                cell = row.CreateCell(cellIndex);
                cell.SetCellValue(data.ElementAt(i).Telefons);
                cellIndex++;

                cell = row.CreateCell(cellIndex);
                cell.SetCellValue(data.ElementAt(i).Email);
                cellIndex++;

                cell = row.CreateCell(cellIndex);
                cell.SetCellValue(data.ElementAt(i).Pilseta);
                cellIndex++;

                cell = row.CreateCell(cellIndex);
                cell.SetCellValue(data.ElementAt(i).SkolasTips);
                cellIndex++;

                cell = row.CreateCell(cellIndex);
                cell.SetCellValue(data.ElementAt(i).SkolasNosaukums);
                cellIndex++;

                cell = row.CreateCell(cellIndex);
                cell.SetCellValue(data.ElementAt(i).IrIzveletaSkola);
                cellIndex++;

                cell = row.CreateCell(cellIndex);
                cell.SetCellValue(data.ElementAt(i).SkolasKlase);
                cellIndex++;

                cell = row.CreateCell(cellIndex);
                cell.SetCellValue(data.ElementAt(i).Kopnite);
                cellIndex++;

                cell = row.CreateCell(cellIndex);
                cell.SetCellValue(data.ElementAt(i).KopnitesTips);
                cellIndex++;

                cell = row.CreateCell(cellIndex);
                cell.SetCellValue(data.ElementAt(i).Created.ToShortDateString());
                cellIndex++;

                rowIndex++;
            }
        }


    }
}