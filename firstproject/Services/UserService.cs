using firstproject.Exceptions;
using firstproject.Models.Domain;
using firstproject.Models.DTOs;
using firstproject.Repositories;

namespace firstproject.Services;
public class UserService {

 public UserRepository _userRepository;
 public PasswordService _passwordService;
 public readonly TokenService _tokenService;

    public UserService(UserRepository userRepository, PasswordService passwordService, TokenService tokenService){
        _userRepository = userRepository;
        _passwordService = passwordService;
        _tokenService = tokenService;
    }

    public async Task<User> Create(CreateUserDto user)
    {
        User? userWithEmail = await _userRepository.FindByEmail(user.Email);
        if(userWithEmail != null)
        {
            throw new EmailAlreadyInUseException("Email already in use.");
        }

        user.Password = _passwordService.generateHash(user.Password);

        User newUser = await _userRepository.CreateUser(user.toEntity());
        return newUser;
    }

    public async Task<string> Login(UserSignInDto credentials)
    {
        User? userWithEmail = await _userRepository.FindByEmail(credentials.Email);
        if(userWithEmail == null) throw new InvalidCredentialsException("Invalid Credentials. Please check your email and password.");

        if(!_passwordService.verifyPassword(credentials.Password, userWithEmail.Password)) throw new InvalidCredentialsException("Invalid Credentials. Please check your email and password.");

        return _tokenService.GenerateToken(userWithEmail.Id);
    }

    public async Task<User?> FindById(long userId)
    {
        User? user = await _userRepository.FindById(userId);
        if(user == null) throw new UserNotFoundException("No user was found.");

        return user;
    }
}