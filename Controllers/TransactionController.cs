using Microsoft.AspNetCore.Mvc;
using Npgsql;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly string _conn;

    public TransactionController(IConfiguration config)
    {
        _conn = config.GetConnectionString("koneksi");
    }

    [HttpGet]
    public IActionResult Get()
    {
        var list = new List<object>();

        using var conn = new NpgsqlConnection(_conn);
        conn.Open();

        var cmd = new NpgsqlCommand("SELECT * FROM transactions WHERE deleted_at IS NULL", conn);
        var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            list.Add(new
            {
                id = reader["id"],
                customer = reader["customer_name"],
                vehicle = reader["vehicle"],
                serviceId = reader["service_id"],
                mechanicId = reader["mechanic_id"],
                date = reader["service_date"]
            });
        }

        return Ok(new { status = "success", data = list });
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        using var conn = new NpgsqlConnection(_conn);
        conn.Open();

        var cmd = new NpgsqlCommand("SELECT * FROM transactions WHERE id=@id", conn);
        cmd.Parameters.AddWithValue("id", id);

        var reader = cmd.ExecuteReader();

        if (!reader.Read())
            return NotFound(new { status = "error", message = "Data tidak ditemukan" });

        return Ok(new
        {
            status = "success",
            data = new
            {
                id = reader["id"],
                customer = reader["customer_name"],
                serviceId = reader["service_id"],
                mechanicId = reader["mechanic_id"],
                date = reader["service_date"]
            }
        });
    }

    [HttpPost]
    public IActionResult Post(Transaction t)
    {
        using var conn = new NpgsqlConnection(_conn);
        conn.Open();

        var cmd = new NpgsqlCommand(
            "INSERT INTO transactions(customer_name,vehicle,service_id,mechanic_id,service_date) VALUES(@c,@v,@s,@m,@d)",
            conn);

        cmd.Parameters.AddWithValue("c", t.CustomerName);
        cmd.Parameters.AddWithValue("v", t.Vehicle);
        cmd.Parameters.AddWithValue("s", t.ServiceId);
        cmd.Parameters.AddWithValue("m", t.MechanicId);
        cmd.Parameters.AddWithValue("d", t.ServiceDate);

        cmd.ExecuteNonQuery();

        return StatusCode(201, new { status = "success", message = "Data ditambahkan" });
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Transaction t)
    {
        using var conn = new NpgsqlConnection(_conn);
        conn.Open();

        try
        {
            var checkCmd = new NpgsqlCommand(
                "SELECT COUNT(*) FROM transactions WHERE id=@id AND deleted_at IS NULL", conn);
            checkCmd.Parameters.AddWithValue("id", id);

            if ((long)checkCmd.ExecuteScalar() == 0)
            {
                return NotFound(new { status = "error", message = "Data tidak ditemukan" });
            }

            var checkService = new NpgsqlCommand(
                "SELECT COUNT(*) FROM services WHERE id=@sid", conn);
            checkService.Parameters.AddWithValue("sid", t.ServiceId);

            if ((long)checkService.ExecuteScalar() == 0)
            {
                return BadRequest(new { status = "error", message = "Service tidak valid" });
            }

            var checkMechanic = new NpgsqlCommand(
                "SELECT COUNT(*) FROM mechanics WHERE id=@mid", conn);
            checkMechanic.Parameters.AddWithValue("mid", t.MechanicId);

            if ((long)checkMechanic.ExecuteScalar() == 0)
            {
                return BadRequest(new { status = "error", message = "Mechanic tidak valid" });
            }

            var cmd = new NpgsqlCommand(@"
            UPDATE transactions 
            SET customer_name=@c,
                vehicle=@v,
                service_id=@s,
                mechanic_id=@m,
                service_date=@d,
                updated_at=NOW()
            WHERE id=@id", conn);

            cmd.Parameters.AddWithValue("id", id);
            cmd.Parameters.AddWithValue("c", t.CustomerName);
            cmd.Parameters.AddWithValue("v", t.Vehicle);
            cmd.Parameters.AddWithValue("s", t.ServiceId);
            cmd.Parameters.AddWithValue("m", t.MechanicId);
            cmd.Parameters.AddWithValue("d", t.ServiceDate);

            cmd.ExecuteNonQuery();

            return Ok(new { status = "success", message = "Data berhasil diupdate" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                status = "error",
                message = ex.Message
            });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        using var conn = new NpgsqlConnection(_conn);
        conn.Open();

        var cmd = new NpgsqlCommand(
            "UPDATE transactions SET deleted_at=NOW() WHERE id=@id AND deleted_at IS NULL",
            conn);

        cmd.Parameters.AddWithValue("id", id);

        int result = cmd.ExecuteNonQuery();

        if (result == 0)
        {
            return NotFound(new
            {
                status = "error",
                message = "Data tidak ditemukan atau sudah dihapus"
            });
        }

        return Ok(new
        {
            status = "success",
            message = "Data berhasil dihapus"
        });

    }
}